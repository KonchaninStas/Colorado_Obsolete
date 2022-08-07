using System.Runtime.InteropServices;

namespace Colorado.OpenGL.OpenGLLibrariesAPI.Helpers
{
    internal static class OpenGLHelperAPI
    {
        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glGetIntegerv")]
        public static extern void GetParameterValuesArray(uint pname, int[] param);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glGetDoublev")]
        public static extern void GetDoublev(int pname, double[] dparams);
    }
}
