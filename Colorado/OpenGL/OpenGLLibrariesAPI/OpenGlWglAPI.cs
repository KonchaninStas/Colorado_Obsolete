using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.OpenGLLibrariesAPI
{
    internal class OpenGlWglAPI
    {
        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "wglMakeCurrent")]
        public static extern int MakeCurrent(IntPtr hDC, IntPtr hRC);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "wglDeleteContext")]
        public static extern int DeleteContext(IntPtr hRC);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "wglCreateContext")]
        public static extern IntPtr CreateContext(IntPtr hDC);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "wglGetProcAddress")]
        public static extern int GetProcAddress(string funcname);
    }
}
