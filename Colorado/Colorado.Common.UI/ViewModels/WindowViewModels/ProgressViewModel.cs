using Colorado.Common.ProgressTracking;
using Colorado.Common.UI.Commands;
using Colorado.Common.UI.ViewModels.Base;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Colorado.Common.UI.ViewModels.WindowViewModels
{
    public class ProgressViewModel : ViewModelBase
    {
        #region Private fields

        private string progressInformation;
        private double progressValue;
        private Visibility visibility;
        private bool isIndeterminate;

        #endregion Private fields

        #region Constructors

        public ProgressViewModel(string title)
        {
            Title = title;
        }

        #endregion Constructors

        #region Properties

        public string Title { get; }

        public bool IsIndeterminate
        {
            get
            {
                return isIndeterminate;
            }
            set
            {
                isIndeterminate = value;
                OnPropertyChanged(nameof(IsIndeterminate));
            }
        }

        public string ProgressInformation
        {
            get
            {
                return progressInformation;
            }
            set
            {
                progressInformation = value;
                OnPropertyChanged(nameof(ProgressInformation));
            }
        }

        public double ProgressValue
        {
            get
            {
                return progressValue;
            }
            set
            {
                progressValue = value;
                OnPropertyChanged(nameof(ProgressValue));
            }
        }

        public Visibility Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }

        public Action CloseAction { get; set; }

        #endregion Properties

        #region Commands

        public ICommand CancelCommand
        {
            get { return new CommandHandler(Cancel); }
        }

        public ICommand CloseCommand
        {
            get { return new CommandHandler<CancelEventArgs>(Close); }
        }

        #endregion  Commands

        #region Public logic

        public void Init()
        {
            ProgressValue = default(int);
            ProgressInformation = default(string);
            ProgressTracker.Instance.IsCanceled = false;
            Visibility = Visibility.Visible;
        }

        #endregion Public logic

        #region Private logic

        private void Cancel()
        {
            CloseAction();
        }

        /// <summary>
        /// Collapses the Progress window. This window will be collapsed, but will not be closed.
        /// </summary>
        /// <param name="args"></param>
        private void Close(CancelEventArgs args)
        {
            args.Cancel = true;
            Visibility = Visibility.Collapsed;
            ProgressTracker.Instance.IsCanceled = true;
        }

        #endregion Private logic
    }
}