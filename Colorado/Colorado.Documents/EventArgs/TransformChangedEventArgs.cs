using Colorado.GeometryDataStructures.Primitives;

namespace Colorado.Documents.EventArgs
{
    public class TransformChangedEventArgs : System.EventArgs
    {
        public TransformChangedEventArgs(Document document, Transform appliedTransform)
        {
            Document = document;
            AppliedTransform = appliedTransform;
        }

        public Document Document { get; }

        public Transform AppliedTransform { get; }
    }
}
