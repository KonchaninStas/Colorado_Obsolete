using System;
using System.Windows.Input;

namespace Colorado.Common.UI.Commands
{
    /// <summary>
    /// Implementation of ICommand interface methods for
    /// using commands instead events in Views
    /// </summary>
    public class CommandHandler : ICommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of CommandHandler class
        /// </summary>
        public CommandHandler(Action execute)
            : this(execute, null) { }

        public CommandHandler(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            this.canExecute = canExecute;
        }

        #endregion Constructors

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        #endregion ICommand Members

        #region Private fields

        private readonly Action _execute;

        private readonly Func<bool> canExecute;

        #endregion Private fields
    }

    public class CommandHandler<T> : ICommand
    {
        #region Constructors

        public CommandHandler(Action<T> execute)
            : this(execute, null) { }

        /// <param name="execute">Action that executes by the command</param>
        /// <param name="canExecute">The execution condition</param>
        public CommandHandler(Action<T> execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion Constructors

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);

        }

        #endregion ICommand Members

        #region Private fields

        private readonly Action<T> _execute;

        private readonly Func<bool> _canExecute;

        #endregion Private fields
    }
}