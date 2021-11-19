using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.Enumerations;
using Colorado.OpenGL.OpenGLLibrariesAPI;
using Colorado.OpenGL.OpenGLLibrariesAPI.View;
using Colorado.OpenGL.OpenGLWrappers.Helpers;
using Colorado.OpenGL.Structures;

namespace Colorado.OpenGL.OpenGLWrappers.View
{
    public static class OpenGLViewportWrapper
    {
        public static void SetViewport(int x, int y, int width, int height)
        {
            OpenGLViewportAPI.Viewport(x, y, width, height);
        }

        public static void SetOrthographicViewSettings(double left, double right, double bottom, double top, double zNear, double zFar)
        {
            OpenGLViewportAPI.Ortho(left, right, bottom, top, zNear, zFar);
        }

        public static void SetPerspectiveCameraSettings(double verticalFieldOfViewInDegrees, double aspectRatio,
            double distanceToNearPlane, double distanceToFarPlane)
        {
            OpenGLViewportAPI.Perspective(verticalFieldOfViewInDegrees, aspectRatio, distanceToNearPlane, distanceToFarPlane);
        }

        public static void Flush()
        {
            OpenGLAPI.Flush();
        }

        public static void ClearColor(RGB colorToClear)
        {
            float[] valuesInFloat = colorToClear.ToFloat4Array();
            OpenGLViewportAPI.ClearColor(valuesInFloat[0], valuesInFloat[1], valuesInFloat[2], valuesInFloat[3]);
        }

        public static int GetViewportWidth()
        {
            return GetViewport().Width;
        }

        public static int GetViewportHeight()
        {
            return GetViewport().Height;
        }

        public static Viewport GetViewport()
        {
            int[] viewportValues = OpenGLHelper.GetParameterValuesArray(OpenGLCapability.Viewport);
            return new Viewport(viewportValues[0], viewportValues[1],
                viewportValues[2], viewportValues[3]);
        }

        public static Point ScreenToWorld(int x, int y)
        {
            int realY = GetViewportHeight() - y - 1;
            double wX = 0.0;
            double wY = 0.0;
            double wZ = 0.0;
            OpenGLViewportAPI.gluUnProject(x, realY, 1, OpenGLMatrixOperationWrapper.GetModelViewMatrix().Array,
                OpenGLMatrixOperationWrapper.GetProjectionMatrix().Array,
                GetViewport().Array, ref wX, ref wY, ref wZ);

            return new Point(wX, wY, wZ);
        }
    }
}
