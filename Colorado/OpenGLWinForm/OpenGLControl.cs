using Colorado.Common.Utilities;
using Colorado.Documents;
using Colorado.GeometryDataStructures.Colors;
using Colorado.OpenGL.Enumerations;
using Colorado.OpenGL.OpenGLWrappers;
using Colorado.OpenGL.Structures;
using Colorado.OpenGLWinForm.Managers;
using Colorado.OpenGLWinForm.Rendering;
using Colorado.OpenGLWinForm.Rendering.RenderableObjects;
using Colorado.OpenGLWinForm.RenderingControlStructures;
using Colorado.OpenGLWinForm.Tools;
using Colorado.OpenGLWinForm.Utilities;
using System;
using System.Windows.Forms;

namespace Colorado.OpenGLWinForm
{
    public partial class OpenGLControl : UserControl, IRenderingControl
    {
        #region IRenderingControl implementation

        public void RefreshView()
        {
            Refresh();
        }

        #endregion IRenderingControl implementation

        #region Private fields

        private readonly DocumentsManager documentsManager;
        private readonly GeometryRenderer geometryRenderer;
        private readonly ViewCamera viewCamera;
        private readonly LightsManager lightsManager;
        private readonly MouseTool mouseTool;
        private readonly KeyboardTool keyboardTool;

        private Context renderingContext;

        private GridPlane gridPlane;

        #endregion Private fields

        #region Constructor

        public OpenGLControl(DocumentsManager documentsManager)
        {
            InitializeComponent();
            this.documentsManager = documentsManager;
            viewCamera = new ViewCamera();
            lightsManager = new LightsManager(viewCamera);

            geometryRenderer = new GeometryRenderer(documentsManager, lightsManager);

            mouseTool = new MouseTool(this, viewCamera);
            keyboardTool = new KeyboardTool(this, viewCamera);

            SubscribeToEvents();

            FpsCalculator = new FpsCalculator(this);
            BackgroundColor = new RGB(0.8, 0.8, 0.8);
            gridPlane = new GridPlane(5, 100);
        }

        #endregion Constructor

        #region Properties

        public FpsCalculator FpsCalculator { get; }

        public RGB BackgroundColor { get; set; }

        #endregion Properties

        #region Events

        public event EventHandler DrawSceneStarted;

        public event EventHandler DrawSceneFinished;

        #endregion Events

        #region Private logic

        private void SubscribeToEvents()
        {
            documentsManager.DocumentOpened += (s, e) => UpdateRenderingControlSettings();
            documentsManager.DocumentClosed += (s, e) => UpdateRenderingControlSettings();
            Load += (s, e) => InitializeGraphics();
            Paint += (s, e) => DrawScene();
            SizeChanged += (s, e) => SetViewportParameters();
        }

        private void UpdateRenderingControlSettings()
        {
            viewCamera.SetObjectRange(documentsManager.TotalBoundingBox);
            gridPlane = new GridPlane(5, documentsManager.TotalBoundingBox.Diagonal);
            Refresh();
        }

        private void SetViewportParameters()
        {
            viewCamera.Width = ClientRectangle.Width;
            viewCamera.Height = ClientRectangle.Height;
        }

        private bool InitializeGraphics()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
            SetStyle(ControlStyles.Opaque, true);
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

        #endregion Private logic

        #region Rendering logic

        private void DrawScene()
        {
            DrawSceneStarted?.Invoke(this, EventArgs.Empty);
            BeginDrawScene();
            DrawEntities();
            EndDrawScene();
            DrawSceneFinished?.Invoke(this, EventArgs.Empty);
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
            lightsManager.CreateHeadLight();
        }

        private void ApplyCamera()
        {
            // projection scale
            OpenGLWrapper.SetActiveMatrixType(MatrixType.Projection);
            OpenGLWrapper.MakeActiveMatrixIdentity();

            viewCamera.ApplySettings();

            // offset & orientation
            OpenGLWrapper.SetActiveMatrixType(MatrixType.ModelView);
            OpenGLWrapper.MakeActiveMatrixIdentity();

            OpenGLWrapper.RotateCurrentMatrix(-MathUtilities.ConvertRadiansToDegrees(viewCamera.CameraRotation.AngleInRadians),
                viewCamera.CameraRotation.Axis);
            OpenGLWrapper.TranslateCurrentMatrix(viewCamera.Origin.Inverse);
        }

        private void DrawEntities()
        {
            gridPlane?.Draw();
            geometryRenderer.DrawSceneGeometry();
        }

        private void EndDrawScene()
        {
            OpenGLWrapper.Flush();
            renderingContext.SwapBuffers();
        }

        #endregion Rendering logic
    }
}
