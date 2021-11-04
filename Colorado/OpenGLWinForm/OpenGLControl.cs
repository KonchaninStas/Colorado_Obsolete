using Colorado.Common.Helpers;
using Colorado.Common.Utilities;
using Colorado.DataStructures;
using Colorado.Documents;
using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry2D;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.Enumerations;
using Colorado.OpenGL.OpenGLWrappers;
using Colorado.OpenGL.Structures;
using Colorado.OpenGLWinForm.Enumerations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Point = Colorado.GeometryDataStructures.Primitives.Point;

namespace Colorado.OpenGLWinForm
{
    public partial class OpenGLControl : UserControl, IRenderingControl
    {
        #region IRenderingControl implementation

        private DocumentsManager documentsManager;

        public void RefreshView()
        {
            Refresh();
        }

        public void SetDocumentManager(DocumentsManager documentsManager)
        {
            this.documentsManager = documentsManager;
            documentsManager.DocumentOpened += (s, e) => UpdateRenderingControlSettings();
            documentsManager.DocumentClosed += (s, e) => UpdateRenderingControlSettings();
        }

        private void UpdateRenderingControlSettings()
        {
            viewCamera.SetObjectRange(documentsManager.TotalBoundingBox);
            gridPlane = new GridPlane(5, documentsManager.TotalBoundingBox.Diagonal);
            Refresh();
        }

        #endregion IRenderingControl implementation

        #region Private fields

        private readonly ViewCamera viewCamera;
        private readonly MouseTool mouseTool;
        private readonly KeyboardTool keyboardTool;

        private Context renderingContext;

        private Transform _ModelViewMatrix;
        private Transform _ProjectionMatrix;
        private Transform _World2NormalizedDeviceCoordinateTransform;

        private GridPlane gridPlane;

        #endregion Private fields

        #region Constructor

        public OpenGLControl(DocumentsManager documentsManager)
        {
            InitializeComponent();
            viewCamera = new ViewCamera();
            mouseTool = new MouseTool(this, viewCamera);
            keyboardTool = new KeyboardTool(this, viewCamera);

            SubscribeToEvents();

            BackgroundColor = new RGBA(206, 206, 206);
            gridPlane = new GridPlane(5, 100);
        }

        #endregion Constructor

        #region Properties

        public RGBA BackgroundColor { get; set; }

        #endregion Properties

        #region Private methods

        private void SubscribeToEvents()
        {
            Load += (s, e) => InitializeGraphics();
            Paint += (s, e) => DrawScene();

            SizeChanged += (s, e) => SetViewportParameters();
        }

        #endregion Private methods

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

        private void SetViewportParameters()
        {
            viewCamera.Width = ClientRectangle.Width;
            viewCamera.Height = ClientRectangle.Height;
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
            double startTime = SysTime;

            BeginDrawScene();
            DrawEntities();
            EndDrawScene();

            double render_time = SysTime - startTime;
            int fps = render_time == 0.0 ? 30 : (int)(1.0 / render_time + 0.5);
            Console.WriteLine(fps);
        }

        private void DrawEntities()
        {
            //OpenGLGeometryWrapper.DrawPoint(Point.ZeroPoint, RGBA.BlueColor, 1);
            OpenGLGeometryWrapper.DrawPoint(Point.ZeroPoint, RGBA.RedColor, 1);
            OpenGLGeometryWrapper.DrawLine(
                new Line(Point.ZeroPoint, Point.ZeroPoint + Vector.XAxis * 100), RGBA.RedColor);
            OpenGLGeometryWrapper.DrawLine(
                new Line(Point.ZeroPoint, Point.ZeroPoint + Vector.YAxis * 100), RGBA.GreenColor);
            OpenGLGeometryWrapper.DrawLine(
               new Line(Point.ZeroPoint, Point.ZeroPoint + Vector.ZAxis * 100), RGBA.BlueColor);
            //if (PointUnderMouse != null)
            //{
            //    OpenGLGeometryWrapper.DrawPoint(PointUnderMouse, RGBA.RedColor, 10);
            //}
            gridPlane?.Draw();
            if (activeDocument != null)
            {
                foreach (GeometryObject geometryObject in activeDocument.Geometries)
                {
                    OpenGLGeometryWrapper.DrawGeometryObject(geometryObject);
                }
            }

        }

        public void BeginDrawScene()
        {
            renderingContext.MakeCurrent();
            OpenGLWrapper.EnableCapability(OpenGLCapability.DepthTest);
            OpenGLWrapper.ClearColor(BackgroundColor);
            OpenGLWrapper.ClearDepthBufferValue();
            OpenGLWrapper.ClearBuffers(OpenGLBufferType.Color, OpenGLBufferType.Depth);
            OpenGLWrapper.SetViewport(0, 0, viewCamera.Width, viewCamera.Height);
            ApplyCamera();
            CreateHeadLight();
        }

        private void ApplyCamera()
        {
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
                OpenGLWrapper.SetOrthographicViewSettings(xmin * viewCamera.Scale, xmax * viewCamera.Scale,
                    ymin * viewCamera.Scale, ymax * viewCamera.Scale, viewCamera.NearClip, viewCamera.FarClip);
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
            OpenGLWrapper.RotateCurrentMatrix(-MathUtilities.ConvertRadiansToDegrees(viewCamera.CameraRotation.AngleInRadians), viewCamera.CameraRotation.Axis);

            _ModelViewMatrix = OpenGLWrapper.GetModelViewMatrix();
            OpenGLWrapper.TranslateCurrentMatrix(origin.Inverse);
            //OpenGLWrapper.ScaleCurrentMatrix(viewCamera.Scale);
            _ModelViewMatrix = _ModelViewMatrix * new Transform(origin.Inverse.ToVector());
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
