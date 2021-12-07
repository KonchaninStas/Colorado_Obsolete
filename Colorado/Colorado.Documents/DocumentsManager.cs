using Colorado.Common.Exceptions;
using Colorado.Common.ProgressTracking;
using Colorado.Common.UI.Handlers;
using Colorado.Documents.EventArgs;
using Colorado.Documents.Properties;
using Colorado.Documents.STL;
using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry3D;
using Colorado.GeometryDataStructures.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Colorado.Documents
{
    public class DocumentsManager
    {
        private readonly List<Document> documents;

        public BoundingBox TotalBoundingBox { get; }

        public IEnumerable<Document> DocumentsToRender => documents.Where(d => d.Visible);

        public int DocumentsCount => documents.Count;

         static DocumentsManager()
        {
            RegisteredFilters = new List<string>();
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(STLDocument).TypeHandle);
        }

        public DocumentsManager()
        {
            documents = new List<Document>();
            TotalBoundingBox = new BoundingBox();
        }

        public void AddDocument(Document document)
        {
            ProgressHandler progressHandler = new ProgressHandler(string.Format(Resources.OpeningDocument, document.Name));
            try
            {
                ProgressTracker.Instance.ShowWindow();
                document.ImportGeometry();

                ProgressTracker.Instance.StartIndeterminate(Resources.VerticesTransformation);
                document.PrepareLocalTransform();

                ProgressTracker.Instance.StartIndeterminate(Resources.BoundingBoxCalculation);
                documents.Add(document);
                TotalBoundingBox.Add(document.BoundingBox);
                DocumentOpened?.Invoke(this, new DocumentOpenedEventArgs(document));
                document.DocumentTransformation.TransformChanged += (s, e) =>
                {
                    RecalculateBoundingBox(true);
                    DocumentUpdated?.Invoke(this, System.EventArgs.Empty);
                };
                
                DocumentsCountChanged?.Invoke(this, System.EventArgs.Empty);
            }
            catch (OperationAbortException)
            {
            }
            catch (Exception ex)
            {
                MessageViewHandler.ShowExceptionMessage(Resources.OpeningDocument, ex);
            }
            finally
            {
                progressHandler.Abort();
            }
        }

        public void CloseDocument(Document documentToClose)
        {
            documents.Remove(documentToClose);
            RecalculateBoundingBox(true);
            DocumentClosed?.Invoke(this, new DocumentClosedEventArgs(documentToClose));
            DocumentsCountChanged?.Invoke(this, System.EventArgs.Empty);
        }

        public void CloseAllDocuments()
        {
            var closedDocuments = new List<Document>(documents);
            documents.Clear();
            TotalBoundingBox.ResetToDefault();
            AllDocumentsClosed?.Invoke(this, new AllDocumentsClosedEventArgs(closedDocuments));
            DocumentsCountChanged?.Invoke(this, System.EventArgs.Empty);
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

        public static ICollection<string> RegisteredFilters { get; }

        public static void RegisterFilter(string filter)
        {
            RegisteredFilters.Add(filter);
        }

        public event EventHandler<DocumentOpenedEventArgs> DocumentOpened;

        public event EventHandler<DocumentClosedEventArgs> DocumentClosed;

        public event EventHandler DocumentsCountChanged;

        public event EventHandler<AllDocumentsClosedEventArgs> AllDocumentsClosed;

        public event EventHandler DocumentUpdated;

        private void RecalculateBoundingBox(bool invokeEvent)
        {
            TotalBoundingBox.ResetToDefault();
            foreach (Document document in documents)
            {
                TotalBoundingBox.Add(document.BoundingBox);
                if (invokeEvent)
                {
                    document.BoundingBox.Updated += (s, args) => DocumentUpdated?.Invoke(this, System.EventArgs.Empty);
                }
            }
        }
    }
}
