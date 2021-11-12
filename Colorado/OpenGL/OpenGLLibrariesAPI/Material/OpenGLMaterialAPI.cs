using System.Runtime.InteropServices;

namespace Colorado.OpenGL.OpenGLLibrariesAPI.Material
{
    internal static class OpenGLMaterialAPI
    {
        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glMaterialf")]
        public static extern void Materialf(int face, int pname, float param);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glMaterialfv")]
        public static extern void Materialfv(int face, int pname, float[] fparams);
    }
}
