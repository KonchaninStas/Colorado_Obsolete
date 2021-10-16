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

    }
}
