using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.OpenGLLibrariesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGL.OpenGLWrappers
{
    public class OpenGLUWrapper
    {
        public static void SetPerspectiveCameraSettings(double verticalFieldOfViewInDegrees, double aspectRatio,
            double distanceToNearPlane, double distanceToFarPlane)
        {
            OpenGLUAPI.Perspective(verticalFieldOfViewInDegrees, aspectRatio, distanceToNearPlane, distanceToFarPlane);
        }

        public static Point ScreenToWorld(int x, int y)
        {
            int realY = OpenGLWrapper.GetViewportHeight() - y - 1;
            double wX = 0.0;
            double wY = 0.0;
            double wZ = 0.0;
            OpenGLUAPI.gluUnProject(x, realY, 1, OpenGLWrapper.GetModelViewMatrix().Array, OpenGLWrapper.GetProjectionMatrix().Array,
                OpenGLWrapper.GetViewport(), ref wX, ref wY, ref wZ);

            return new Point(wX, wY, wZ);
        }
    }
}
