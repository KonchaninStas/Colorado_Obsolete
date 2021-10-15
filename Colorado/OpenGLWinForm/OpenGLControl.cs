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
using System.Runtime.InteropServices;
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
            Load += LoadCallback;
            SizeChanged += SizeChangedCallback;
            Paint += PaintCallback;

            BackgroundColor = new RGBA();
        }
        private const string GL_DLL = "opengl32";
        [DllImport(GL_DLL)]
        private static extern void glBegin(uint mode);
        [DllImport(GL_DLL)]
        private static extern void glEnd();

        [DllImport(GL_DLL)]
        private static extern void glVertex3d(double x, double y, double z);
        private void PaintCallback(object sender, PaintEventArgs e)
        {
            DrawScene();
        }

        private void LoadCallback(object sender, EventArgs e)
        {
            InitializeGraphics();
        }

        private void SizeChangedCallback(object sender, EventArgs e)
        {
            // Not remove
            OpenGLWrapper.SetViewport(0, 0, Width, Height);
            //OpenGLWrapper.SetOrthographicViewSettings(-1.0, 1.0, -1.0, 1.0, 1.0, 100.0);
        }

        public RGBA BackgroundColor { get; set; }

        /// <summary>
        /// Respond to the <see cref="E:System.Windows.Forms.Control.Paint"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.DesignMode)
            {
                e.Graphics.Clear(this.BackColor);
                e.Graphics.Flush();
                return;
            }
            base.OnPaint(e);
        }

        /// <summary>
        /// Responds to the OnPaintBackground event.
        /// Override is to disable the parent's implementation, do nothing.
        /// </summary>
        /// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that contains information about the control to paint.</param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

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
                renderingContext = new Context(Handle, 32, 32, 8);
                OpenGLWrapper.ClearColor(BackgroundColor);
                OpenGLWrapper.SetShadingMode(ShadingModel.Smooth);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void DrawScene()
        {
            BeginDrawScene();
            DrawEntities();
            EndDrawScene();
        }

        private void DrawEntities()
        {
            glBegin(0x0001);
            glVertex3d(.25, 0.25, 0.25);
            glVertex3d(100.75, 100.75, 100.75);
            glEnd();
        }

        public void BeginDrawScene()
        {
            renderingContext.MakeCurrent();
            OpenGLWrapper.EnableCapability(OpenGLCapability.DepthTest);
            OpenGLWrapper.ClearColor(BackgroundColor);
            OpenGLWrapper.ClearDepthBufferValue();
            OpenGLWrapper.ClearBuffers(OpenGLBufferType.Color, OpenGLBufferType.Depth);
            OpenGLWrapper.SetViewport(0, 0, this.Width, this.Height);
            //ApplyCamera();
            //CreateHeadLight();
        }

        public void EndDrawScene()
        {
            OpenGLWrapper.Flush();
            renderingContext.SwapBuffers();
        }
    }
}
