using Colorado.Common.Enumerations;
using Colorado.Common.Tools.Keyboard;
using Colorado.Common.Utilities;
using Colorado.Documents.EventArgs;
using Colorado.Documents.Utilities;
using Colorado.GeometryDataStructures.Primitives;
using System;

namespace Colorado.Documents
{
    public class DocumentTransformation
    {
        #region Fields

        private readonly Document document;
        private readonly KeyboardToolsManager keyboardToolsManager;
        private readonly DocumentTransformationEditor documentTransformationEditor;

        #endregion Fields

        #region Constructor

        public DocumentTransformation(Document document, KeyboardToolsManager keyboardToolsManager)
        {
            this.document = document;
            this.keyboardToolsManager = keyboardToolsManager;
            ActiveTransform = Transform.Identity();
            documentTransformationEditor = new DocumentTransformationEditor(this);
            InitialTransform = Transform.Identity();
        }

        #endregion Constructor

        #region Properties

        public Transform InitialTransform { get; set; }

        public Transform ActiveTransform { get; private set; }

        public bool CanBeMovedToOrigin => !document.BoundingBox.Center.IsZero;

        public bool CanBeRestoredToDefault => !ActiveTransform.IsIdentity();

        public bool IsEditing { get; private set; }

        public bool RotateAroundGlobalOrigin { get; set; }

        #endregion Properties

        #region Events

        public event EventHandler<DocumentEditingStartedEventArgs> EditingStarted;

        public event EventHandler<DocumentEditingFinishedEventArgs> EditingFinished;

        public event EventHandler<TransformChangedEventArgs> TransformChanged;

        #endregion Events

        #region Public logic

        public void StartEditing()
        {
            IsEditing = true;
            EditingStarted?.Invoke(this, new DocumentEditingStartedEventArgs(document));
            keyboardToolsManager.RegisterKeyboardToolHandler(documentTransformationEditor);
        }

        public void FinishEditing()
        {
            IsEditing = false;
            EditingFinished?.Invoke(this, new DocumentEditingFinishedEventArgs(document));
            keyboardToolsManager.UnregisterKeyboardToolHandler(documentTransformationEditor);
        }

        public void Move(MoveDirection moveDirection, double speed)
        {
            switch (moveDirection)
            {
                case MoveDirection.Forward:
                    {
                        ApplyTransform(Transform.CreateTranslation(Vector.XAxis * speed));
                        break;
                    }
                case MoveDirection.Backward:
                    {
                        ApplyTransform(Transform.CreateTranslation(Vector.XAxis * speed).GetInverted());
                        break;
                    }
                case MoveDirection.Up:
                    {
                        ApplyTransform(Transform.CreateTranslation(Vector.ZAxis * speed));
                        break;
                    }
                case MoveDirection.Down:
                    {
                        ApplyTransform(Transform.CreateTranslation(Vector.ZAxis * speed).GetInverted());
                        break;
                    }

                case MoveDirection.Right:
                    {
                        ApplyTransform(Transform.CreateTranslation(Vector.YAxis * speed));
                        break;
                    }
                case MoveDirection.Left:
                    {
                        ApplyTransform(Transform.CreateTranslation(Vector.YAxis * speed).GetInverted());
                        break;
                    }
            }
        }

        public void RotateAroundCenter(Vector rotationAxis, double rotationAngleInDegrees)
        {
            var moveToOriginTransform = Transform.CreateTranslation(document.BoundingBox.Center.Inverse.ToVector());
            if (!RotateAroundGlobalOrigin)
            {
                ApplyTransform(moveToOriginTransform, false);
            }

            Vector translationVector = ActiveTransform.Translation;
            Transform resultTransform = Transform.CreateTranslation(translationVector.Inverse);
            resultTransform = resultTransform.Multiply(
                Transform.CreateFromAxisAngle(rotationAxis, MathUtilities.ConvertDegreesToRadians(rotationAngleInDegrees)));
            resultTransform.Translate(translationVector);
            ApplyTransform(resultTransform, RotateAroundGlobalOrigin);

            if (!RotateAroundGlobalOrigin)
            {
                ApplyTransform(moveToOriginTransform.GetInverted(), true);
            }
        }

        public void ApplyTransform(Transform anotherTransform, bool invokeEvent = true)
        {
            ActiveTransform = ActiveTransform.Multiply(anotherTransform);
            document.BoundingBox.ApplyTransform(anotherTransform);
            if (invokeEvent)
            {
                TransformChanged?.Invoke(this, new TransformChangedEventArgs(document, anotherTransform));
            }
        }

        public void Scale(double scaleFactor)
        {
            ApplyTransform(Transform.CreateScale(scaleFactor));
        }

        public void RestoreDefaultTransform()
        {
            ApplyTransform(ActiveTransform.GetInverted(), false);
            ApplyTransform(InitialTransform);
        }

        public void MoveToOrigin()
        {
            ApplyTransform(Transform.CreateTranslation(document.BoundingBox.Center.Inverse.ToVector()));
        }

        #endregion Public logic
    }
}
