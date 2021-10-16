using Colorado.OpenGL.Structures;
using Colorado.OpenGL.WindowsAPI.WindowsAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGL.WindowsAPI.WindowsAPIWrappers
{
    class Gdi32LibraryWrapper
    {
        public static int ChoosePixelFormat(IntPtr deviceContextHandle, PixelFormatDescriptor pixelFormatDescriptor)
        {
            return Gdi32LibraryAPI.ChoosePixelFormat(deviceContextHandle, pixelFormatDescriptor);
        }

        internal static void SetPixelFormat(IntPtr deviceContextHandle, int pixelFormat, PixelFormatDescriptor pixelFormatDescriptor)
        {
            Gdi32LibraryAPI.SetPixelFormat(deviceContextHandle, pixelFormat, pixelFormatDescriptor);
        }

        public static void SwapBuffers(IntPtr deviceContextHandle)
        {
            Gdi32LibraryAPI.SwapBuffers(deviceContextHandle);
        }
    }
}
