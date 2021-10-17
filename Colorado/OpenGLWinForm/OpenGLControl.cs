using Colorado.Common.Utilities;
using Colorado.DataStructures;
using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry2D;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.Enumerations;
using Colorado.OpenGL.OpenGLWrappers;
using Colorado.OpenGL.Structures;
using Colorado.OpenGLWinForm.Enumerations;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Point = Colorado.GeometryDataStructures.Primitives.Point;

namespace Colorado.OpenGLWinForm
{
    public partial class OpenGLControl : UserControl
    {
        private const double LARGE_OFFSET = 10000.0;      // in internal unit

        private readonly ViewCamera viewCamera;
        private readonly MouseTool mouseTool;
        private readonly KeyboardTool keyboardTool;

        private Context renderingContext;
        private Document activeDocument;

        private Transform _ModelViewMatrix;
        private Transform _ProjectionMatrix;
        private Transform _World2NormalizedDeviceCoordinateTransform;

        // For supporting large coordinate values efficiently
        private bool _HasLargeOffset;
        private Vector _LargeOffset;

        public OpenGLControl()
        {
            InitializeComponent();
            viewCamera = new ViewCamera();
            mouseTool = new MouseTool(this, viewCamera);
            keyboardTool = new KeyboardTool(this, viewCamera);

            AddDocument(new Document());
            activeDocument.AddGeometryObject(new Line(new Point(.25, 0.25, 0.25), new Point(100.75, 100.75, 100.75)));

            Load += LoadCallback;
            SizeChanged += SizeChangedCallback;
            Paint += PaintCallback;

            BackgroundColor = new RGBA();
        }

        public Point PointUnderMouse => mouseTool.PointUnderMouse;

        public void AddDocument(Document document)
        {
            activeDocument = document;
        }

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
            if (this.DesignMode)
            {
                base.OnResize(e);
                return;
            }

            viewCamera.Width = this.ClientRectangle.Width;
            viewCamera.Height = this.ClientRectangle.Height;

            base.OnResize(e);
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
            OpenGLGeometryWrapper.DrawPoint(viewCamera.Target, RGBA.RedColor, 10);

            if (PointUnderMouse != null)
            {
                OpenGLGeometryWrapper.DrawPoint(PointUnderMouse, RGBA.RedColor, 10);
            }

            foreach (GeometryObject geometryObject in activeDocument.Geometries)
            {
                OpenGLGeometryWrapper.DrawGeometryObject(geometryObject);
            }
        }

        public void BeginDrawScene()
        {
            renderingContext.MakeCurrent();
            OpenGLWrapper.EnableCapability(OpenGLCapability.DepthTest);
            OpenGLWrapper.ClearColor(BackgroundColor);
            OpenGLWrapper.ClearDepthBufferValue();
            OpenGLWrapper.ClearBuffers(OpenGLBufferType.Color, OpenGLBufferType.Depth);
            OpenGLWrapper.SetViewport(0, 0, this.Width, this.Height);
            ApplyCamera();
            CreateHeadLight();
        }

        private void ApplyCamera()
        {
            viewCamera.SetObjectRange(activeDocument.BoundingBox);
            // projection scale

            OpenGLWrapper.SetActiveMatrixType(MatrixType.Projection);
            OpenGLWrapper.MakeActiveMatrixIdentity();
            if (viewCamera.CameraType == CameraType.Orthographic)
            {
                Vector2D imageSize = viewCamera.ImageSize;
                double xmin = -imageSize.X / 2;
                double xmax = imageSize.X / 2;
                double ymin = -imageSize.Y / 2;
                double ymax = imageSize.Y / 2;
                OpenGLWrapper.SetOrthographicViewSettings(xmin, xmax, ymin, ymax, viewCamera.NearClip, viewCamera.FarClip);
            }
            else
            {
                OpenGLUWrapper.SetPerspectiveCameraSettings(viewCamera.VerticalFieldOfViewInDegrees, viewCamera.AspectRatio,
                    viewCamera.NearClip, viewCamera.FarClip);
            }
            _ProjectionMatrix = OpenGLWrapper.GetProjectionMatrix();
            // offset & orientation
            OpenGLWrapper.SetActiveMatrixType(MatrixType.ModelView);
            OpenGLWrapper.MakeActiveMatrixIdentity();

            Point origin = viewCamera.Origin;
            Vector inversedOrigin = origin.ToVector().Inverse;
            Quaternion rotation = viewCamera.Quaternion;
            OpenGLWrapper.RotateCurrentMatrix(-MathUtilities.ConvertRadiansToDegrees(rotation.W), rotation.Axis);
            _ModelViewMatrix = OpenGLWrapper.GetModelViewMatrix();
            OpenGLWrapper.TranslateCurrentMatrix(inversedOrigin);

            var transOriginTransform = new Transform(inversedOrigin);
            _ModelViewMatrix = _ModelViewMatrix * transOriginTransform;
            // If points have large coordinate values, will reset camera origin
            // and have future points compensate the origin.
            if (origin.LargestAbsoluteComponent > LARGE_OFFSET)
            {
                _HasLargeOffset = true;
                _LargeOffset = new Vector(-origin.X, -origin.Y, -origin.Z);
                OpenGLWrapper.TranslateCurrentMatrix(origin);

            }
            else
            {
                _HasLargeOffset = false;
                _LargeOffset = Vector.ZeroVector;
            }
            // Compute the transformation from world to camera NDC.
            _World2NormalizedDeviceCoordinateTransform = _ProjectionMatrix * _ModelViewMatrix;
        }

        protected void CreateHeadLight()
        {
            OpenGLWrapper.EnableLight(LightType.Light0);
            OpenGLWrapper.SetLightParameter(LightType.Light0, LightParameter.ConstantAttenuation, 1.0f);
            OpenGLWrapper.SetLightParameter(LightType.Light0, LightParameter.LinearAttenuation, 0.0f);
            OpenGLWrapper.SetLightParameter(LightType.Light0, LightParameter.QuadraticAttenuation, 0.0f);
            var color = new float[]
            {
                0.2f,
                0.2f,
                0.2f,
                1.0f
            };

            OpenGLWrapper.SetLightParameter(LightType.Light0, LightColorType.Ambient, color);
            color[0] = 1.0f;
            color[1] = 1.0f;
            color[2] = 1.0f;
            OpenGLWrapper.SetLightParameter(LightType.Light0, LightColorType.Diffuse, color);
            OpenGLWrapper.SetLightParameter(LightType.Light0, LightColorType.Specular, color);

            var position = new float[] { 0, 0, 0, 1 };
            if (viewCamera.CameraType == CameraType.Orthographic)
            {
                Point cameraOrigin = viewCamera.Origin;
                Vector cameraDir = viewCamera.ViewDirection;
                cameraOrigin = cameraOrigin - cameraDir * 1E10;
                position[0] = (float)(cameraOrigin.X + _LargeOffset.X);
                position[1] = (float)(cameraOrigin.Y + _LargeOffset.Y);
                position[2] = (float)(cameraOrigin.Z + _LargeOffset.Z);
                position[3] = 1.0f;
            }
            else
            {
                Point cameraOrigin = viewCamera.Origin;
                position[0] = (float)(cameraOrigin.X + _LargeOffset.X);
                position[1] = (float)(cameraOrigin.Y + _LargeOffset.Y);
                position[2] = (float)(cameraOrigin.Z + _LargeOffset.Z);
                position[3] = 1.0f;
            }
            OpenGLWrapper.SetLightParameter(LightType.Light0, LightParameter.Position, 0.0f);
        }

        public void EndDrawScene()
        {
            OpenGLWrapper.Flush();
            renderingContext.SwapBuffers();
        }
    }
}
