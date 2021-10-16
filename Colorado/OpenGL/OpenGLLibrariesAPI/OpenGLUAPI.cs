using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGL.OpenGLLibrariesAPI
{
    internal class OpenGLUAPI
    {
        private const string GLUDLLName = "GLU32.DLL";
        [DllImport(GLUDLLName, EntryPoint = "gluPerspective")]
        public static extern void Perspective(double fovy, double aspect, double zNear, double zFar);
    }
}
