using System;
using System.Runtime.InteropServices;

namespace Colorado.OpenGL.WindowsAPI.WindowsAPI
{
    class User32LibraryAPI
    {
        [DllImport("User32.dll", SetLastError = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);
    }
}
