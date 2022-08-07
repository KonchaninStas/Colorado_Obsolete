using System;
using System.Runtime.InteropServices;

namespace Colorado.OpenGL.WindowsAPI.WindowsAPI
{
    class Kernel32LibraryAPI
    {
        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibrary(String funcname);
    }
}
