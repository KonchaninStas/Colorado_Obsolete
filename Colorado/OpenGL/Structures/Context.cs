using Colorado.OpenGL.Extensions;
using Colorado.OpenGL.OpenGLWrappers;
using Colorado.OpenGL.WindowsAPI.WindowsAPIWrappers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGL.Structures
{
    public class Context : IDisposable
    {
        private readonly IntPtr openGLControlHandle;
        private IntPtr deviceContext;
        private IntPtr renderingContext;
        private IntPtr windowHandle;


        public Context(IntPtr hwnd, byte color, byte depth, byte stencil)
        {
            Boolean initialLoad = false;
            windowHandle = hwnd;
            if (openGLControlHandle == IntPtr.Zero)
            {
                openGLControlHandle = OpenGLWrapper.LoadOpenGLLibrary();
                initialLoad = true;
            }
            deviceContext = User32LibraryWrapper.GetDeviceContext(windowHandle);
            PixelFormatDescriptor pfd = new PixelFormatDescriptor();
            pfd.ColorBits = color;
            pfd.DepthBits = depth;
            pfd.StencilBits = stencil;
            int nPixelFormat = Gdi32LibraryWrapper.ChoosePixelFormat(deviceContext, pfd);
            if (nPixelFormat == 0)
            {
                Debug.WriteLine("ChoosePixelFormat failed.");
                return;
            }

            Gdi32LibraryWrapper.SetPixelFormat(deviceContext, nPixelFormat, pfd);
            renderingContext = OpenGlWglWrapper.CreateContext(deviceContext);
            MakeCurrent();
            if (initialLoad)
            {
                OpenGLExtensionsWrapper.LoadExtensions();
            }
        }

        public IntPtr DeviceContext
        {
            get
            {
                return deviceContext;
            }
        }

        public IntPtr RenderingContext
        {
            get
            {
                return renderingContext;
            }
        }

        public void MakeCurrent()
        {
            OpenGlWglWrapper.SetCurrentRenderingContext(deviceContext, renderingContext);
        }

        public void SwapBuffers()
        {
            Gdi32LibraryWrapper.SwapBuffers(deviceContext);
        }

        public void Dispose()
        {
            if (renderingContext != IntPtr.Zero)
            {
                OpenGlWglWrapper.DeleteOpenGLRenderingContext(renderingContext);
                renderingContext = IntPtr.Zero;
            }
            if (deviceContext != IntPtr.Zero)
            {
                User32LibraryWrapper.ReleaseDeviceContext(windowHandle, deviceContext);
                deviceContext = IntPtr.Zero;
            }
        }
    }
}