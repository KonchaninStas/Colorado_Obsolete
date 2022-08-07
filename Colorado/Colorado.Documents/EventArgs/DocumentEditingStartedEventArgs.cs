namespace Colorado.Documents.EventArgs
{
    public class DocumentEditingStartedEventArgs : System.EventArgs
    {
        public DocumentEditingStartedEventArgs(Document editingDocument)
        {
            EditingDocument = editingDocument;
        }

        public Document EditingDocument { get; }
    }
}
