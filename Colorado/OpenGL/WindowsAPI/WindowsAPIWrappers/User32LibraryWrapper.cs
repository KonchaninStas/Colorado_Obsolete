using Colorado.OpenGL.WindowsAPI.WindowsAPI;
using System;

namespace Colorado.OpenGL.WindowsAPI.WindowsAPIWrappers
{
    class User32LibraryWrapper
    {
        public static int ReleaseDeviceContext(IntPtr windowHandle, IntPtr deviceContextHandle)
        {
            return User32LibraryAPI.ReleaseDC(windowHandle, deviceContextHandle);
        }

        public static IntPtr GetDeviceContext(IntPtr windowHandle)
        {
            return User32LibraryAPI.GetDC(windowHandle);
        }
    }
}
