using Colorado.OpenGL.OpenGLLibrariesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGL.OpenGLWrappers
{
    class OpenGlWglWrapper
    {
        internal static IntPtr CreateContext(IntPtr deviceContextHandle)
        {
            return OpenGlWglAPI.CreateContext(deviceContextHandle);
        }

        public static int SetCurrentRenderingContext(IntPtr deviceContextHandle, IntPtr renderingContextHandle)
        {
            return OpenGlWglAPI.MakeCurrent(deviceContextHandle, renderingContextHandle);
        }

        public static int DeleteOpenGLRenderingContext(IntPtr renderingContextHandle)
        {
            return OpenGlWglAPI.DeleteContext(renderingContextHandle);
        }

        public static IntPtr CreateOpenGLRenderingContext(IntPtr deviceContextHandle)
        {
            return OpenGlWglAPI.CreateContext(deviceContextHandle);
        }

        public static int GetExtensionFunctionAddress(string functionName)
        {
            return OpenGlWglAPI.GetProcAddress(functionName);
        }
    }
}
