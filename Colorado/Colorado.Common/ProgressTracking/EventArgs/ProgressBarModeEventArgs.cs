namespace Colorado.Common.ProgressTracking.EventArgs
{
    public class ProgressBarModeEventArgs : System.EventArgs
    {
        public ProgressBarModeEventArgs(bool isIndeterminate)
        {
            IsIndeterminate = isIndeterminate;
        }

        public bool IsIndeterminate { get; }
    }
}
