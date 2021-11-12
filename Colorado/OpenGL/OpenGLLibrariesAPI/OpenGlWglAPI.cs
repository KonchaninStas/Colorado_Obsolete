using System;
using System.Runtime.InteropServices;

namespace Colorado.OpenGL.OpenGLLibrariesAPI
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
