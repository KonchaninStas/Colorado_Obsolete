using GeometryDataStructures.Colors;
using OpenGL.Enumerations;
using OpenGL.OpenGLLibrariesAPI;
using OpenGL.WindowsAPI;
using OpenGL.WindowsAPI.WindowsAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.OpenGLWrappers
{
    public static class OpenGLWrapper
    {
        public static IntPtr LoadOpenGLLibrary()
        {
            return Kernel32LibraryAPI.LoadLibrary(OpenGLLibraryNames.OpenGLLibraryName);
        }

        public static void ClearColor(RGBA rgbaColor)
        {
            OpenGLAPI.ClearColor(rgbaColor.Red / byte.MaxValue, rgbaColor.Green / byte.MaxValue,
                rgbaColor.Blue / byte.MaxValue, rgbaColor.Alpha / byte.MaxValue);
        }

        public static void SetShadingMode(ShadingModel shadingModel)
        {
            OpenGLAPI.ShadeModel((int)shadingModel);
        }

        public static void SetViewport(int x, int y, int width, int height)
        {
            OpenGLAPI.Viewport(x, y, width, height);
        }

        public static void SetOrthographicViewSettings(double left, double right, double bottom, double top, double zNear, double zFar)
        {
            OpenGLAPI.Ortho(left, right, bottom, top, zNear, zFar);
        }
    }
}
