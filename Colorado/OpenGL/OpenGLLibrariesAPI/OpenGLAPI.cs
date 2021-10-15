using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.OpenGLLibrariesAPI
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
        [DllImport(DLLName, EntryPoint = "glClearDepth")]
        public static extern void ClearDepth(double depth);
        [DllImport(DLLName, EntryPoint = "glClear")]
        public static extern void Clear(int mask);

        [DllImport(DLLName, EntryPoint = "glFlush")]
        public static extern void Flush();
    }
}
