using System.Runtime.InteropServices;

namespace Colorado.OpenGL.OpenGLLibrariesAPI.Light
{
    internal static class OpenGLGlobalLightAPI
    {
        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glLightModeli")]
        public static extern void LightModeli(int pname, int param);


        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glLightModelfv")]
        public static extern void LightModelfv(int pname, float[] fparams);
    }
}
