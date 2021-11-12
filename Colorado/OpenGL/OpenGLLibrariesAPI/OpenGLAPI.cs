using System.Runtime.InteropServices;

namespace Colorado.OpenGL.OpenGLLibrariesAPI
{
    internal class OpenGLAPI
    {
        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glShadeModel")]
        public static extern void ShadeModel(int mode);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glEnable")]
        public static extern void Enable(int cap);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glIsEnabled")]
        public static extern bool IsEnabled(int cap);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glDisable")]
        public static extern void Disable(int cap);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glClearDepth")]
        public static extern void ClearDepth(double depth);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glClear")]
        public static extern void Clear(int mask);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glFlush")]
        public static extern void Flush();
    }
}
