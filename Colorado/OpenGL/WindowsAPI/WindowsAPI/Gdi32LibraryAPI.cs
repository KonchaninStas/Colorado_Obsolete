using Colorado.OpenGL.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGL.WindowsAPI.WindowsAPI
{
    class Gdi32LibraryAPI
    {
        [DllImport("GDI32.dll", SetLastError = true)]
        public static extern int ChoosePixelFormat(IntPtr hDC, [In, MarshalAs(UnmanagedType.LPStruct)] PixelFormatDescriptor pfd);

        [DllImport("GDI32.dll", SetLastError = true)]
        public static extern bool SetPixelFormat(IntPtr hDC, int format, [In, MarshalAs(UnmanagedType.LPStruct)] PixelFormatDescriptor pfd);

        [DllImport("GDI32.dll", SetLastError = true)]
        public static extern void SwapBuffers(IntPtr hDC);
    }
}
