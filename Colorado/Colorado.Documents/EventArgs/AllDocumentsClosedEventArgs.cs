using System.Collections.Generic;

namespace Colorado.Documents.EventArgs
{
    public class AllDocumentsClosedEventArgs : System.EventArgs
    {
        public AllDocumentsClosedEventArgs(IEnumerable<Document> closedDocuments)
        {
            ClosedDocuments = closedDocuments;
        }

        public IEnumerable<Document> ClosedDocuments { get; }
    }
}
