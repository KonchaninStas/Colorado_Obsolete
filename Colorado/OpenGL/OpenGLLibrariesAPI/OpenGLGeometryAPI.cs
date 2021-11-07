using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGL.OpenGLLibrariesAPI
{
    internal class OpenGLGeometryAPI
    {
        private const string GL_DLL = "opengl32";

        [DllImport(GL_DLL, EntryPoint = "glBegin")]
        public static extern void glBegin(uint mode);

        [DllImport(GL_DLL, EntryPoint = "glEnd")]
        public static extern void glEnd();

        [DllImport(GL_DLL, EntryPoint = "glPointSize")]
        public static extern void glPointSize(float size);


        [DllImport(GL_DLL, EntryPoint = "glVertex3d")]
        public static extern void glVertex3d(double x, double y, double z);

        [DllImport(GL_DLL, EntryPoint = "glNormal3d")]
        public static extern void glNormal3d(double x, double y, double z);

        [DllImport(GL_DLL, EntryPoint = "glColor3fv")]
        public static extern void glColor3fv(float[] color);

        [DllImport(GL_DLL, EntryPoint = "glDrawArrays")]
        public static extern void DrawArrays(int mode, int first, int count);

        #region Fast
        [DllImport(GL_DLL, EntryPoint = "glEnableClientState")]
        public static extern void EnableClientState(int array);

        [DllImport(GL_DLL, EntryPoint = "glDisableClientState")]
        public static extern void DisableClientState(int array);


        [DllImport(GL_DLL, EntryPoint = "glVertexPointer")]
        public static extern void VertexPointer(int size, int type, int stride, IntPtr pointer);

        [DllImport(GL_DLL, EntryPoint = "glNormalPointer")]
        public static extern void NormalPointer(int type, int stride, IntPtr pointer);

        [DllImport(GL_DLL, EntryPoint = "glColorPointer")]
        public static extern void ColorPointer(int size, int type, int stride, IntPtr pointer);

      

        #endregion Fast
    }
}
