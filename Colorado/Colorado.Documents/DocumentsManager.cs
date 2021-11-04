using Colorado.Documents.EventArgs;
using Colorado.GeometryDataStructures.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.Documents
{
    public class DocumentsManager
    {
        private readonly IEnumerable<Document> documents;

        public BoundingBox TotalBoundingBox { get; set; }

        public DocumentsManager()
        {
            documents = new List<Document>();
        }

        public void AddDocument(Document document)
        {

        }

        public event EventHandler<DocumentOpenedEventArgs> DocumentOpened;

        public event EventHandler<DocumentClosedEventArgs> DocumentClosed;
    }
}
