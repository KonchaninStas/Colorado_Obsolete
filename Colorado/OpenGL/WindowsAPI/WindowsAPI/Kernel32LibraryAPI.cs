using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.WindowsAPI.WindowsAPI
{
    class Kernel32LibraryAPI
    {
        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibrary(String funcname);
    }
}
