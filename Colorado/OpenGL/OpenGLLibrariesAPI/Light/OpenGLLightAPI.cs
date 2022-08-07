using System.Runtime.InteropServices;

namespace Colorado.OpenGL.OpenGLLibrariesAPI.Light
{
    internal static class OpenGLLightAPI
    {
        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glLightf")]
        public static extern void Lightf(int light, int pname, float param);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glLightfv")]
        public static extern void Lightfv(int light, int pname, float[] fparams);
    }
}
