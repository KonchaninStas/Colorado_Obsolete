using Colorado.Documents.EventArgs;
using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
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
        private readonly List<Document> documents;

        public BoundingBox TotalBoundingBox { get; set; }
        public List<GeometryObject> GeometryToRender { get; }

        public DocumentsManager()
        {
            documents = new List<Document>();
            GeometryToRender = new List<GeometryObject>();
            TotalBoundingBox = new BoundingBox();
        }

        public void AddDocument(Document document)
        {
            documents.Add(document);
            GeometryToRender.AddRange(document.Geometries);
            TotalBoundingBox = TotalBoundingBox.Add(document.BoundingBox);
            DocumentOpened?.Invoke(this, new DocumentOpenedEventArgs(document));
        }

        public event EventHandler<DocumentOpenedEventArgs> DocumentOpened;

        public event EventHandler<DocumentClosedEventArgs> DocumentClosed;
    }
}
