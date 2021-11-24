using System.Runtime.InteropServices;

namespace Colorado.OpenGL.OpenGLLibrariesAPI.View
{
    internal class OpenGLMatrixOperationAPI
    {
        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glMatrixMode")]
        public static extern void MatrixMode(int mode);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glLoadIdentity")]
        public static extern void LoadIdentity();

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glMultMatrixd")]
        public static extern void MultMatrixd(double[] m);


        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glScaled")]
        public static extern void Scale(double x, double y, double z);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glTranslated")]
        public static extern void Translated(double x, double y, double z);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glRotated")]
        public static extern void Rotated(double angle, double x, double y, double z);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glLoadMatrixd")]
        public static extern void LoadMatrixd(double[] m);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glPushMatrix")]
        public static extern void PushMatrix();

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glPopMatrix")]
        public static extern void PopMatrix();
    }
}
