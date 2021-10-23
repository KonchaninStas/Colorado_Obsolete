using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGL.OpenGLLibrariesAPI
{
    internal class OpenGLAPI
    {
        private const string DLLName = OpenGLLibraryNames.OpenGLLibraryName;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="red">0-1</param>
        /// <param name="green">0-1</param>
        /// <param name="blue">0-1</param>
        /// <param name="alpha">0-1</param>
        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glClearColor")]
        public static extern void ClearColor(float red, float green, float blue, float alpha);

        [DllImport(OpenGLLibraryNames.OpenGLLibraryName, EntryPoint = "glShadeModel")]
        public static extern void ShadeModel(int mode);

        [DllImport(DLLName, EntryPoint = "glViewport")]
        public static extern void Viewport(int x, int y, int width, int height);

        [DllImport(DLLName, EntryPoint = "glOrtho")]
        public static extern void Ortho(double left, double right, double bottom, double top, double zNear, double zFar);

        [DllImport(DLLName, EntryPoint = "glEnable")]
        public static extern void Enable(int cap);

        [DllImport(DLLName, EntryPoint = "glDisable")]
        public static extern void Disable(int cap);

        [DllImport(DLLName, EntryPoint = "glClearDepth")]
        public static extern void ClearDepth(double depth);

        [DllImport(DLLName, EntryPoint = "glClear")]
        public static extern void Clear(int mask);


        [DllImport(DLLName, EntryPoint = "glFlush")]
        public static extern void Flush();


        [DllImport(DLLName, EntryPoint = "glMatrixMode")]
        public static extern void MatrixMode(int mode);

        [DllImport(DLLName, EntryPoint = "glLoadIdentity")]
        public static extern void LoadIdentity();

        [DllImport(DLLName, EntryPoint = "glGetDoublev")]
        public static extern void GetDoublev(int pname, double[] dparams);

        [DllImport(DLLName, EntryPoint = "glTranslated")]
        public static extern void Translated(double x, double y, double z);

        [DllImport(DLLName, EntryPoint = "glRotated")]
        public static extern void Rotated(double angle, double x, double y, double z);

        [DllImport(DLLName, EntryPoint = "glLightf")]
        public static extern void Lightf(int light, int pname, float param);

        [DllImport(DLLName, EntryPoint = "glLightfv")]
        public static extern void Lightfv(int light, int pname, float[] fparams);

        [DllImport(DLLName, EntryPoint = "glGetIntegerv")]
        public static extern void GetParameterValuesArray(uint pname, int[] param);

        [DllImport(DLLName, EntryPoint = "glMultMatrixd")]
        public static extern void MultMatrixd(double[] m);


        [DllImport(DLLName, EntryPoint = "glScaled")]
        public static extern void Scale(double x, double y, double z);
    }
}
