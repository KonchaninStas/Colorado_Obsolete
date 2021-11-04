using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Colorado.Common.UI.Commands
{
    public class EventToCommand : TriggerAction<DependencyObject>
    {
        #region Constants

        private const string CommandPropertyName = "Command";
        private const string CommandParameterPropertyName = "CommandParameter";
        private const string MustToggleIsEnabledPropertyName = "MustToggleIsEnabled";
        private const string EventArgsConverterParameterPropertyName = "EventArgsConverterParameter";
        private const string AlwaysInvokeCommandPropertyName = "AlwaysInvokeCommand";

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
           CommandParameterPropertyName,
           typeof(object),
           typeof(EventToCommand),
           new PropertyMetadata(
               null,
               (s, e) => EnableDisableCommand(s)));

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            CommandPropertyName,
            typeof(ICommand),
            typeof(EventToCommand),
            new PropertyMetadata(
                null,
                (s, e) => OnCommandChanged(s as EventToCommand, e)));

        public static readonly DependencyProperty MustToggleIsEnabledProperty = DependencyProperty.Register(
            MustToggleIsEnabledPropertyName,
            typeof(bool),
            typeof(EventToCommand),
            new PropertyMetadata(
                false,
                (s, e) => EnableDisableCommand(s)));

        public static readonly DependencyProperty EventArgsConverterParameterProperty = DependencyProperty.Register(
            EventArgsConverterParameterPropertyName,
            typeof(object),
            typeof(EventToCommand),
            new PropertyMetadata(null));

        public static readonly DependencyProperty AlwaysInvokeCommandProperty = DependencyProperty.Register(
            AlwaysInvokeCommandPropertyName,
            typeof(bool),
            typeof(EventToCommand),
            new PropertyMetadata(false));

        #endregion

        #region Properties

        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }

            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public object CommandParameter
        {
            get
            {
                return GetValue(CommandParameterProperty);
            }

            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        public object CommandParameterValue
        {
            get
            {
                return commandParameterValue ?? CommandParameter;
            }

            set
            {
                commandParameterValue = value;
                EnableDisableElement();
            }
        }

        public bool MustToggleIsEnabled
        {
            get
            {
                return (bool)GetValue(MustToggleIsEnabledProperty);
            }

            set
            {
                SetValue(MustToggleIsEnabledProperty, value);
            }
        }

        public bool MustToggleIsEnabledValue
        {
            get
            {
                return mustToggleValue == null
                           ? MustToggleIsEnabled
                           : mustToggleValue.Value;
            }

            set
            {
                mustToggleValue = value;
                EnableDisableElement();
            }
        }

        public bool PassEventArgsToCommand { get; set; }

        public bool AlwaysInvokeCommand
        {
            get
            {
                return (bool)GetValue(AlwaysInvokeCommandProperty);
            }
            set
            {
                SetValue(AlwaysInvokeCommandProperty, value);
            }
        }

        #endregion

        #region Public logic

        public void Invoke()
        {
            Invoke(null);
        }

        protected override void Invoke(object parameter)
        {
            if (AssociatedElementIsDisabled() && !AlwaysInvokeCommand)
                return;

            object commandParameter = CommandParameterValue;

            if (commandParameter == null && PassEventArgsToCommand)
            {
                commandParameter = parameter;
            }

            if (Command != null
                && Command.CanExecute(commandParameter))
            {
                Command.Execute(commandParameter);
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            EnableDisableElement();
        }

        #endregion

        #region Private logic

        private static void OnCommandChanged(EventToCommand element, DependencyPropertyChangedEventArgs e)
        {
            if (element == null)
                return;

            if (e.OldValue != null)
                ((ICommand)e.OldValue).CanExecuteChanged -= element.OnCommandCanExecuteChanged;

            var command = (ICommand)e.NewValue;

            if (command != null)
                command.CanExecuteChanged += element.OnCommandCanExecuteChanged;

            element.EnableDisableElement();
        }

        private static void EnableDisableCommand(DependencyObject dependencyObject)
        {
            var sender = dependencyObject as EventToCommand;
            if (sender == null)
                return;

            if (sender.AssociatedObject == null)
                return;

            sender.EnableDisableElement();
        }

        private bool AssociatedElementIsDisabled()
        {
            var element = AssociatedObject as FrameworkElement;

            return AssociatedObject == null || (element != null && !element.IsEnabled);
        }

        private void EnableDisableElement()
        {
            var element = AssociatedObject as FrameworkElement;

            if (element == null)
                return;

            if (MustToggleIsEnabledValue && Command != null)
                element.IsEnabled = Command.CanExecute(CommandParameterValue);
        }

        private void OnCommandCanExecuteChanged(object sender, EventArgs e)
        {
            EnableDisableElement();
        }

        #endregion

        #region Private fields

        private object commandParameterValue;
        private bool? mustToggleValue;

        #endregion
    }
}
