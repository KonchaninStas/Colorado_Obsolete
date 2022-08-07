using System;
using System.Runtime.InteropServices;

namespace Colorado.OpenGL.OpenGLLibrariesAPI.Geometry
{
    internal class OpenGLGeometryAPI
    {
        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glBegin")]
        public static extern void glBegin(uint mode);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glEnd")]
        public static extern void glEnd();

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glPointSize")]
        public static extern void glPointSize(float size);


        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glVertex3d")]
        public static extern void glVertex3d(double x, double y, double z);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glNormal3d")]
        public static extern void glNormal3d(double x, double y, double z);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glColor3fv")]
        public static extern void glColor3fv(float[] color);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glDrawArrays")]
        public static extern void DrawArrays(int mode, int first, int count);

        #region Fast

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glEnableClientState")]
        public static extern void EnableClientState(int array);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glDisableClientState")]
        public static extern void DisableClientState(int array);


        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glVertexPointer")]
        public static extern void VertexPointer(int size, int type, int stride, IntPtr pointer);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glNormalPointer")]
        public static extern void NormalPointer(int type, int stride, IntPtr pointer);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glColorPointer")]
        public static extern void ColorPointer(int size, int type, int stride, IntPtr pointer);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glLineWidth")]
        public static extern void LineWidth(float width);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glPolygonMode")]
        public static extern void PolygonMode(int face, int mode);

        #endregion Fast
    }
}
