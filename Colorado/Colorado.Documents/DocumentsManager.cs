using Colorado.Documents.EventArgs;
using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Colorado.Documents
{
    public class DocumentsManager
    {
        private readonly List<Document> documents;

        public BoundingBox TotalBoundingBox { get; set; }

        public IEnumerable<GeometryObject> GeometryToRender => documents.Where(d => d.Visible).SelectMany(d => d.Geometries);

        public int DocumentsCount => documents.Count;

        public DocumentsManager()
        {
            documents = new List<Document>();
            TotalBoundingBox = new BoundingBox();
        }

        public void AddDocument(Document document)
        {
            documents.Add(document);
            TotalBoundingBox = TotalBoundingBox.Add(document.BoundingBox);
            DocumentOpened?.Invoke(this, new DocumentOpenedEventArgs(document));
        }

        public void CloseDocument(Document documentToClose)
        {
            documents.Remove(documentToClose);
            TotalBoundingBox = new BoundingBox();
            foreach (var document in documents)
            {
                TotalBoundingBox = TotalBoundingBox.Add(document.BoundingBox);
            }
            DocumentClosed?.Invoke(this, new DocumentClosedEventArgs(documentToClose));
        }

        public void CloseAllDocuments()
        {
            var closedDocuments = new List<Document>(documents);
            documents.Clear();
            TotalBoundingBox = new BoundingBox();
            AllDocumentsClosed?.Invoke(this, new AllDocumentsClosedEventArgs(closedDocuments));
        }

        public void ShowDocument(Document document)
        {
            document.Visible = true;
        }

        public void HideDocument(Document document)
        {
            document.Visible = false;
        }

        public void IsolateDocument(Document documentToIsolate)
        {
            foreach (Document document in Documents)
            {
                HideDocument(document);
            }
            documentToIsolate.Visible = true;
        }

        public IEnumerable<Document> Documents => documents;

        public event EventHandler<DocumentOpenedEventArgs> DocumentOpened;

        public event EventHandler<DocumentClosedEventArgs> DocumentClosed;

        public event EventHandler<AllDocumentsClosedEventArgs> AllDocumentsClosed;


    }
}
