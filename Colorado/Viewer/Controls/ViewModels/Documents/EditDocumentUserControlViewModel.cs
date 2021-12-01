using Colorado.Common.UI.Commands;
using Colorado.Common.Utilities;
using Colorado.Documents;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGLWinForm;
using Colorado.Viewer.Controls.ViewModels.Common;
using System;
using System.Windows.Input;

namespace Colorado.Viewer.Controls.ViewModels.Documents
{
    public class EditDocumentUserControlViewModel : ViewerBaseViewModel
    {
        private readonly Document document;
        private EulerAngles eulerAngles;

        public EditDocumentUserControlViewModel(IRenderingControl renderingControl, Document document) : base(renderingControl)
        {
            this.document = document;
            document.DocumentTransformation.TransformChanged += TransformChangedCallback;
            TransformChangedCallback(null, null);
        }

        private void TransformChangedCallback(object sender, System.EventArgs e)
        {
            eulerAngles = document.DocumentTransformation.ActiveTransform.ToQuaternion().GetEulerAngles();
            OnPropertyChanged(nameof(PositionX));
            OnPropertyChanged(nameof(PositionY));
            OnPropertyChanged(nameof(PositionZ));

            OnPropertyChanged(nameof(RotationAroundXAxis));
            OnPropertyChanged(nameof(RotationAroundYAxis));
            OnPropertyChanged(nameof(RotationAroundZAxis));

            OnPropertyChanged(nameof(Scale));
        }

        #region Properties

        public double PositionX => Math.Round(document.DocumentTransformation.ActiveTransform.Translation.X, 3);

        public double PositionY => Math.Round(document.DocumentTransformation.ActiveTransform.Translation.Y, 3);

        public double PositionZ => Math.Round(document.DocumentTransformation.ActiveTransform.Translation.Z, 3);

        public double RotationAroundXAxis => Math.Round(MathUtilities.ConvertRadiansToDegrees(eulerAngles.Roll), 3);

        public double RotationAroundYAxis => Math.Round(MathUtilities.ConvertRadiansToDegrees(eulerAngles.Pitch), 3);

        public double RotationAroundZAxis => Math.Round(MathUtilities.ConvertRadiansToDegrees(eulerAngles.Yaw), 3);

        public double Scale => Math.Round(document.DocumentTransformation.ActiveTransform.Scale, 3);

        #endregion Properties

        #region Commands

        public ICommand MoveToOriginCommand
        {
            get
            {
                return new CommandHandler(document.DocumentTransformation.MoveToOrigin,
                    () => document.DocumentTransformation.CanBeMovedToOrigin);
            }
        }

        public ICommand RestoreDefaultTransformCommand
        {
            get
            {
                return new CommandHandler(document.DocumentTransformation.RestoreDefaultTransform,
                    () => document.DocumentTransformation.CanBeRestoredToDefault);
            }
        }

        public ICommand FinishEditingCommand
        {
            get { return new CommandHandler(document.DocumentTransformation.FinishEditing); }
        }

        #endregion Commands
    }
}
