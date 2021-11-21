using Colorado.Common.Utilities;
using Colorado.Documents;
using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry2D;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.Enumerations;
using Colorado.OpenGL.Managers;
using Colorado.OpenGL.Managers.Materials;
using Colorado.OpenGL.OpenGLWrappers;
using Colorado.OpenGL.OpenGLWrappers.Geometry;
using Colorado.OpenGL.OpenGLWrappers.View;
using Colorado.OpenGL.Structures;
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
        private readonly ViewCamera viewCamera;
        private readonly MouseTool mouseTool;
        private readonly KeyboardTool keyboardTool;

        private Context renderingContext;

        #endregion Private fields

        #region Constructor

        public OpenGLControl(DocumentsManager documentsManager)
        {
            InitializeComponent();
            this.documentsManager = documentsManager;
            viewCamera = new ViewCamera();
            LightsManager = new LightsManager();
            DefaultMaterialsManager = DefaultMaterialsManager.Instance;
            GeometryRenderer = new GeometryRenderer(documentsManager, viewCamera);

            mouseTool = new MouseTool(this, viewCamera);
            keyboardTool = new KeyboardTool(this, viewCamera);

            SubscribeToEvents();

            FpsCalculator = new FpsCalculator(this);
            BackgroundColor = new RGB(204, 204, 204);
            UpdateRenderingControlSettings();
        }

        #endregion Constructor

        #region Properties

        public FpsCalculator FpsCalculator { get; }

        public RGB BackgroundColor { get; }

        public LightsManager LightsManager { get; }

        public DefaultMaterialsManager DefaultMaterialsManager { get; }

        public GeometryRenderer GeometryRenderer { get; }

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
            documentsManager.AllDocumentsClosed += (s, e) => UpdateRenderingControlSettings();
            Load += (s, e) => InitializeGraphics();
            Paint += (s, e) => DrawScene();
            SizeChanged += (s, e) => viewCamera.SetViewportParameters(ClientRectangle);
        }

        private void UpdateRenderingControlSettings()
        {
            viewCamera.SetObjectRange(documentsManager.TotalBoundingBox.Add(GeometryRenderer.GridPlane.BoundingBox));

            Refresh();
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
                OpenGLViewportWrapper.ClearColor(BackgroundColor);
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
            OpenGLWrapper.EnableCapability(OpenGLCapability.PointSmooth);
            OpenGLWrapper.EnableCapability(OpenGLCapability.NormalizeNormals);
            OpenGLViewportWrapper.ClearColor(BackgroundColor);
            OpenGLWrapper.ClearDepthBufferValue();
            OpenGLWrapper.ClearBuffers(OpenGLBufferType.Color, OpenGLBufferType.Depth);
            viewCamera.Apply();
        }

        private void DrawEntities()
        {
            LightsManager.DisableLighting();
            GeometryRenderer.DrawGeometryPrimitives();
            LightsManager.DrawLightsSources(documentsManager.TotalBoundingBox.Diagonal, viewCamera.ViewCameraTransform.Scale);
            LightsManager.ConfigureEnabledLights();
            GeometryRenderer.DrawSceneGeometry();
        }

        private void EndDrawScene()
        {
            OpenGLWrapper.Flush();
            renderingContext.SwapBuffers();
        }

        #endregion Rendering logic
    }
}
