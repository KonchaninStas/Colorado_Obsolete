using Colorado.GeometryDataStructures.Primitives;

namespace Colorado.OpenGLWinForm.RenderingControlStructures
{
    public class ViewCameraTransform
    {
        #region Constructor

        public ViewCameraTransform()
        {
            ResetToDefault();
        }

        #endregion Constructor

        #region Properties

        public double Scale { get; private set; }

        public Vector Translation { get; private set; }

        public Quaternion CameraRotation { get; private set; }

        #endregion Properties

        #region Public logic

        public void ResetToDefault()
        {
            Scale = 1;
            Translation = Vector.ZeroVector;
            CameraRotation = Quaternion.Identity;

            RotateAroundTarget(Vector.XAxis, 65);
            RotateAroundTarget(Vector.ZAxis, 45);
        }

        public void ScaleAtTarget(double scale)
        {
            Scale *= scale;
        }

        public void TranslateOrigin(Vector translationVector)
        {
            Translation += translationVector;
        }

        public void RotateAroundTarget(Vector rotationAxis, double angleInDegrees)
        {
            RotateAroundTarget(Quaternion.Create(rotationAxis, angleInDegrees));
        }

        public void RotateAroundTarget(Quaternion quaternion)
        {
            CameraRotation = quaternion * CameraRotation;
        }

        #endregion Public logic
    }
}
