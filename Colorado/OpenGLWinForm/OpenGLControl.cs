using GeometryDataStructures.Colors;
using OpenGL.Enumerations;
using OpenGL.OpenGLWrappers;
using OpenGL.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenGLWinForm
{
    public partial class OpenGLControl : UserControl
    {
        private Context renderingContext;

        public OpenGLControl()
        {
            InitializeComponent();
            InitializeGraphics();
        }

        public RGBA BackgroundColor { get; set; }

        /// <summary>
        /// Initialize the rendering context / device for graphics
        /// Call this after the control is created but before displayed
        /// </summary>
        /// <returns><c>true</c> if successful; otherwise <c>false</c> if rendering device fails to initialize.</returns>
        //[EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        private bool InitializeGraphics()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // ignore the WM_ERASEBACKGROUND emssage, to reduce blinking
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);  // not using optimized double buffer?
            SetStyle(ControlStyles.Opaque, true);               // not displaying background
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            try
            {
                renderingContext = new Context(this.Handle, 32, 32, 8);
                OpenGLWrapper.ClearColor(BackgroundColor);
                OpenGLWrapper.SetShadingMode(ShadingModel.Smooth);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
