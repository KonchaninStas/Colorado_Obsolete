using Colorado.GeometryDataStructures.Primitives;

namespace Colorado.Documents
{
    public class DocumentTransformation
    {
        #region Fields

        private readonly BoundingBox initialBoundingBox;

        private BoundingBox currentBoundingBox;
        private Transform documentTransform;
        private Transform movedToOriginDocumentTransform;

        #endregion Fields

        #region Constructor

        public DocumentTransformation(BoundingBox boundingBox)
        {
            initialBoundingBox = boundingBox.Clone();
            currentBoundingBox = boundingBox;
            documentTransform = Transform.Identity();
        }

        #endregion Constructor

        #region Properties

        public Transform ActiveTransform => WasMovedToOrigin ? movedToOriginDocumentTransform : documentTransform;

        public bool WasMovedToOrigin { get; private set; }

        public bool CanBeMovedToOrigin => !initialBoundingBox.Center.IsZero;

        #endregion Properties

        #region Public logic

        public void RestoreDefaultPosition()
        {
            WasMovedToOrigin = false;
            currentBoundingBox.ApplyTransform(movedToOriginDocumentTransform.GetInverted());
        }

        public void MoveToOrigin()
        {
            WasMovedToOrigin = true;
            movedToOriginDocumentTransform = documentTransform.Clone();
            movedToOriginDocumentTransform.SetTranslation(currentBoundingBox.Center.Inverse);
            currentBoundingBox.ApplyTransform(movedToOriginDocumentTransform);
        }

        #endregion Public logic
    }
}
