namespace Colorado.Documents.EventArgs
{
    public class DocumentEditingFinishedEventArgs : System.EventArgs
    {
        public DocumentEditingFinishedEventArgs(Document editedDocument)
        {
            EditedDocument = editedDocument;
        }

        public Document EditedDocument { get; }
    }
}
