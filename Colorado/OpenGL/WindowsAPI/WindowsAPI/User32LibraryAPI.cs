using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.WindowsAPI.WindowsAPI
{
    class User32LibraryAPI
    {
        [DllImport("User32.dll", SetLastError = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);
    }
}
