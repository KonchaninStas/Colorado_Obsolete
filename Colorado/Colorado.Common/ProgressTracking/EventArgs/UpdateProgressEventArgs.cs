namespace Colorado.Common.ProgressTracking.EventArgs
{
    public class UpdateProgressEventArgs : System.EventArgs
    {
        public UpdateProgressEventArgs(double value, string information)
        {
            Value = value;
            Information = information;
        }

        public double Value { get; }

        public string  Information { get; }
    }
}
