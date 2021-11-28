using Colorado.Common.ProgressTracking;
using Colorado.Common.ProgressTracking.EventArgs;
using Colorado.Common.UI.ViewModels.WindowViewModels;
using Colorado.Common.UI.Windows;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Colorado.Common.UI.Handlers
{
    public class ProgressHandler
    {
        #region Private fields

        private readonly string title;
        private ProgressView progressWindow;
        private ProgressViewModel progressViewModel;
        private Thread uiThread;
        private AutoResetEvent progressWindowWaitHandle;

        #endregion Private fields

        #region Constructors

        public ProgressHandler(string title)
        {
            this.title = title;
            CreateExportWindow();
            SubscribeToEvents();
        }

        #endregion Constructors

        #region Public logic

        /// <summary>
        /// Closes the Progress window and aborts the current UI thread.
        /// This method should be called before the application closes.
        /// </summary>
        public void Abort()
        {
            if (uiThread != null)
            {
                UnsubscribeFromEvents();
                progressWindow.Dispatcher.InvokeShutdown();
            }
        }

        #endregion Public logic

        #region Private logic

        private void SubscribeToEvents()
        {
            ProgressTracker.Instance.OnUpdateProgress += OnUpdateProgressCallback;
            ProgressTracker.Instance.OnProgressBarModeChanged += OnProgressBarModeChanged;
            ProgressTracker.Instance.OnShowWindow += ShowProgressWindowCallback;
            ProgressTracker.Instance.OnCloseWindow += CloseProgressWindowCallback;
        }

        private void UnsubscribeFromEvents()
        {
            ProgressTracker.Instance.OnUpdateProgress -= OnUpdateProgressCallback;
            ProgressTracker.Instance.OnProgressBarModeChanged -= OnProgressBarModeChanged;
            ProgressTracker.Instance.OnShowWindow -= ShowProgressWindowCallback;
            ProgressTracker.Instance.OnCloseWindow -= CloseProgressWindowCallback;
        }

        private void OnUpdateProgressCallback(object sender, UpdateProgressEventArgs args)
        {
            if (progressViewModel == null)
            {
                return;
            }

            progressViewModel.ProgressValue = args.Value;
            progressViewModel.ProgressInformation = args.Information;
        }

        private void OnProgressBarModeChanged(object sender, ProgressBarModeEventArgs args)
        {
            if (progressViewModel == null)
            {
                return;
            }
            progressViewModel.IsIndeterminate = args.IsIndeterminate;
        }

        private void CreateExportWindow()
        {
            if (progressViewModel != null)
                return;

            uiThread = StartInSeparateThread(new ThreadStart(OpenProgressWindow));
        }

        private void OpenProgressWindow()
        {
            progressWindow = new ProgressView(title);
            progressViewModel = (ProgressViewModel)progressWindow.DataContext;
            progressViewModel.Visibility = Visibility.Hidden;

            progressWindow.Show();
            progressWindowWaitHandle?.Set();
            Dispatcher.Run();
        }

        private Thread StartInSeparateThread(ThreadStart threadStart)
        {
            using (progressWindowWaitHandle = new AutoResetEvent(false))
            {
                var progressThread = new Thread(threadStart);
                progressThread.SetApartmentState(ApartmentState.STA);
                progressThread.IsBackground = true;
                progressThread.Start();
                progressWindowWaitHandle.WaitOne();
                return progressThread;
            }
        }

        private void ShowProgressWindowCallback(object sender, System.EventArgs e)
        {
            progressViewModel.Init();
        }

        private void CloseProgressWindowCallback(object sender, System.EventArgs e)
        {
            progressViewModel.Visibility = Visibility.Hidden;
            ProgressTracker.Instance.IsCanceled = false;
        }

        #endregion
    }
}