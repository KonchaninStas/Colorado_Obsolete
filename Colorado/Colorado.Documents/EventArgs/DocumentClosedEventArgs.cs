namespace Colorado.Documents.EventArgs
{
    public class DocumentClosedEventArgs : System.EventArgs
    {
        public DocumentClosedEventArgs(Document closedDocument)
        {
            ClosedDocument = closedDocument;
        }

        public Document ClosedDocument { get; }
    }
}
