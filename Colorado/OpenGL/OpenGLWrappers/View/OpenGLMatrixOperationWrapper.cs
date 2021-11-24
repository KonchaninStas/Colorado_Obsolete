using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.Enumerations;
using Colorado.OpenGL.OpenGLLibrariesAPI.View;
using Colorado.OpenGL.OpenGLWrappers.Helpers;

namespace Colorado.OpenGL.OpenGLWrappers.View
{
    public static class OpenGLMatrixOperationWrapper
    {
        public static Transform GetModelViewMatrix()
        {
            return new Transform(OpenGLHelper.GetParameterValues(ViewMatrixArrayType.ModelView));
        }

        public static Transform GetProjectionMatrix()
        {
            return new Transform(OpenGLHelper.GetParameterValues(ViewMatrixArrayType.Projection));
        }

        public static void TranslateCurrentMatrix(Point point)
        {
            OpenGLMatrixOperationAPI.Translated(point.X, point.Y, point.Z);
        }

        public static void TranslateCurrentMatrix(Vector vector)
        {
            OpenGLMatrixOperationAPI.Translated(vector.X, vector.Y, vector.Z);
        }

        public static void MultiplyWithCurrentMatrix(Transform transform)
        {
            OpenGLMatrixOperationAPI.MultMatrixd(transform.Array);
        }

        public static void LoadMatrix(Transform transform)
        {
            OpenGLMatrixOperationAPI.LoadMatrixd(transform.Array);
        }

        public static void RotateCurrentMatrix(double rotationAngleInDegrees, Vector rotationVector)
        {
            OpenGLMatrixOperationAPI.Rotated(rotationAngleInDegrees,
                rotationVector.X, rotationVector.Y, rotationVector.Z);
        }

        public static void ScaleCurrentMatrix(double scale)
        {
            OpenGLMatrixOperationAPI.Scale(scale, scale, scale);
        }

        public static void SetActiveMatrixType(MatrixType matrixType)
        {
            OpenGLMatrixOperationAPI.MatrixMode((int)matrixType);
        }

        public static void MakeActiveMatrixIdentity()
        {
            OpenGLMatrixOperationAPI.LoadIdentity();
        }
    }
}
