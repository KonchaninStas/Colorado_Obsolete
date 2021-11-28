using Colorado.Common.Exceptions;
using Colorado.Common.ProgressTracking.EventArgs;
using System;

namespace Colorado.Common.ProgressTracking
{
    public class ProgressTracker
    {
        #region Private fields

        private static ProgressTracker instance;
        private int totalStepsCount;
        private double processedStepsCount;
        private bool isIndeterminate;

        #endregion Private fields

        #region Constructors

        private ProgressTracker() { }

        #endregion Constructors

        #region Properties

        public static ProgressTracker Instance
        {
            get
            {
                if (instance == null)
                    instance = new ProgressTracker();

                return instance;
            }
        }

        public bool IsCanceled { get; set; }

        public bool IsIndeterminate
        {
            get
            {
                return isIndeterminate;
            }
            private set
            {
                isIndeterminate = value;
                OnProgressBarModeChanged.Invoke(this, new ProgressBarModeEventArgs(isIndeterminate));
            }
        }

        #endregion Properties

        #region Events

        public event EventHandler<UpdateProgressEventArgs> OnUpdateProgress;

        public event EventHandler<ProgressBarModeEventArgs> OnProgressBarModeChanged;

        public event EventHandler OnShowWindow;

        public event EventHandler OnCloseWindow;

        #endregion

        #region Public logic

        public void Init(int totalStepsCount)
        {
            this.totalStepsCount = totalStepsCount;
            processedStepsCount = 0;
            IsIndeterminate = false;
        }

        public void StartIndeterminate(string information = null)
        {
            ThrowIfCanceled();
            OnUpdateProgress?.Invoke(this, new UpdateProgressEventArgs(0, information));
            IsIndeterminate = true;
        }

        public void NextStep(string information = null)
        {
            ThrowIfCanceled();
            OnUpdateProgress?.Invoke(this, new UpdateProgressEventArgs(processedStepsCount++ / totalStepsCount * 100, information));
        }

        public void ShowWindow()
        {
            OnShowWindow?.Invoke(this, System.EventArgs.Empty);
            StartIndeterminate();
        }

        public void CloseWindow()
        {
            OnCloseWindow?.Invoke(this, System.EventArgs.Empty);
        }

        public void ThrowIfCanceled()
        {
            if (IsCanceled)
            {
                throw new OperationAbortException();
            }
        }

        #endregion Public logic
    }
}
