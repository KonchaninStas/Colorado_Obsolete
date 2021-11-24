using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Colorado.Common.UI.Controls
{
    /// <summary>
    /// Interaction logic for SliderWithNumberBox.xaml
    /// </summary>
    public partial class SliderWithNumberBox : UserControl
    {
        #region Fields

        private bool isSliderUpdating;
        private int lastSavedValue;

        #endregion Fields

        #region Constructor

        public SliderWithNumberBox()
        {
            InitializeComponent();
            Value = 0;
            Slider.PreviewMouseUp += PreviewMouseUpCallback;
            Slider.PreviewMouseDown += PreviewMouseDownCallback;
            NumberBox.ValueChanged += NumberBox_ValueChanged;
        }

        #endregion Constructor

        #region Events

        public event EventHandler ValueChanged;

        #endregion Events

        #region Dependency properties

        public Brush SliderBackgroundColor
        {
            get { return (Brush)GetValue(SliderBackgroundColorProperty); }
            set { SetValue(SliderBackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty SliderBackgroundColorProperty =
            DependencyProperty.Register(nameof(SliderBackgroundColorProperty), typeof(Brush),
              typeof(SliderWithNumberBox), new PropertyMetadata(default(Brush)));

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(nameof(Maximum), typeof(int),
              typeof(SliderWithNumberBox), new PropertyMetadata(0));

        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(nameof(Minimum), typeof(int),
              typeof(SliderWithNumberBox), new PropertyMetadata(100));

        [System.ComponentModel.Bindable(true, BindingDirection.TwoWay)]
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set
            {
                NumberBox.Text = value.ToString();
                SetValue(ValueProperty, value);
            }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(int),
              typeof(SliderWithNumberBox), new PropertyMetadata(0));

        #endregion Dependency properties

        #region Event handlers

        private void PreviewMouseDownCallback(object sender, MouseButtonEventArgs e)
        {
            lastSavedValue = Value;
            isSliderUpdating = true;
        }

        private void PreviewMouseUpCallback(object sender, MouseButtonEventArgs e)
        {
            isSliderUpdating = false;
            if (lastSavedValue != Value)
            {
                ValueChangedEventInvoke();
            }
        }

        #endregion Event handlers

        #region Protected logic

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == MaximumProperty)
            {
                Slider.Maximum = Maximum;
                NumberBox.Maximum = Maximum;
            }
            else if (e.Property == MinimumProperty)
            {
                Slider.Minimum = Minimum;
                NumberBox.Minimum = Minimum;
            }
            else if (e.Property == ValueProperty)
            {
                Slider.Value = Value;
                NumberBox.Text = Value.ToString();
            }
            else if (e.Property == SliderBackgroundColorProperty)
            {
                Slider.Background = SliderBackgroundColor;
            }
        }

        #endregion Protected logic

        #region Private logic

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            NumberBox.Text = ((int)e.NewValue).ToString();
        }

        private void NumberBox_ValueChanged(object sender, EventArgs e)
        {
            int newValue = NumberBox.Value;
            if (Value == 0 && newValue != 0)
            {
                Value = int.Parse(newValue.ToString().Replace("0", string.Empty));
                NumberBox.CaretIndex = NumberBox.Text.Length;
            }
            else
            {
                Value = newValue;
            }
            ValueChangedEventInvoke();
        }

        private void ValueChangedEventInvoke()
        {
            if (!isSliderUpdating)
            {
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion Private logic
    }
}
