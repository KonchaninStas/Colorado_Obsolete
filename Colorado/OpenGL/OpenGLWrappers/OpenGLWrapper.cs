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

        public static void EnableCapability(OpenGLCapability capability)
        {
            OpenGLAPI.Enable((int)capability);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newDepthValue">Should be in range [0,1].</param>
        public static void ClearDepthBufferValue(double newDepthValue)
        {
            OpenGLAPI.ClearDepth(newDepthValue);
        }

        public static void ClearDepthBufferValue()
        {
            OpenGLAPI.ClearDepth(1);
        }

        public static void Flush()
        {
            OpenGLAPI.Flush();
        }

        public static void ClearBuffers(params OpenGLBufferType[] bufferTypes)
        {
            foreach (OpenGLBufferType bufferType in bufferTypes)
            {
                ClearBuffer(bufferType);
            }
        }

        public static void ClearBuffer(OpenGLBufferType bufferType)
        {
            OpenGLAPI.Clear((int)bufferType);
        }
    }
}
