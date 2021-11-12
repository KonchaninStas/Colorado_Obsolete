using Colorado.OpenGL.Enumerations;
using Colorado.OpenGL.OpenGLLibrariesAPI;
using Colorado.OpenGL.WindowsAPI.WindowsAPI;
using System;

namespace Colorado.OpenGL.OpenGLWrappers
{
    public static class OpenGLWrapper
    {
        public static IntPtr LoadOpenGLLibrary()
        {
            return Kernel32LibraryAPI.LoadLibrary(OpenGLLibraryNames.OpenGLLibraryName);
        }

        public static void SetShadingMode(ShadingModel shadingModel)
        {
            OpenGLAPI.ShadeModel((int)shadingModel);
        }

        public static void EnableCapability(OpenGLCapability capability)
        {
            OpenGLAPI.Enable((int)capability);
        }

        public static void DisableCapability(OpenGLCapability capability)
        {
            OpenGLAPI.Disable((int)capability);
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
