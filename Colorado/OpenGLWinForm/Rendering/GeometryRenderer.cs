using Colorado.Documents;
using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry2D;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.OpenGLWrappers.Geometry;
using Colorado.OpenGLWinForm.Rendering.RenderableObjects;
using Colorado.OpenGLWinForm.View;

namespace Colorado.OpenGLWinForm.Rendering
{
    public class GeometryRenderer
    {
        #region Private fields

        private readonly DocumentsManager documentsManager;
        private readonly Camera viewCamera;

        #endregion Private fields

        #region Constructor

        public GeometryRenderer(DocumentsManager documentsManager, Camera viewCamera)
        {
            this.documentsManager = documentsManager;
            this.viewCamera = viewCamera;
            GlobalMaterial = Material.Default;
            DrawCoordinateSystem = true;
            TargetPointColor = RGB.TargetPointDefaultColor;
            SubscribeToEvents();
            UpdateRenderingControlSettings();
            CoordinateSystemAxisLength = 100;
        }

        #endregion Constructor 

        #region Properties

        public GridPlane GridPlane { get; private set; }

        public bool UseGlobalMaterial { get; set; }

        public Material GlobalMaterial { get; set; }

        public bool DrawCoordinateSystem { get; set; }

        public bool DrawTargetPoint { get; set; }

        public RGB TargetPointColor { get; }

        public double CoordinateSystemAxisLength { get; set; }

        #endregion Properties

        #region Public logic

        public void DrawGeometryPrimitives()
        {
            GridPlane.Draw();
            if (DrawTargetPoint)
            {
                OpenGLGeometryWrapper.DrawPoint(viewCamera.TargetPoint.Inverse, TargetPointColor, 20);
            }
            if (DrawCoordinateSystem)
            {
                DrawOriginCoordinateSystem();
            }
        }

        public void DrawSceneGeometry()
        {
            DrawEntities();
        }

        #endregion Public logic

        #region Private logic

        private void SubscribeToEvents()
        {
            documentsManager.DocumentOpened += (s, e) => UpdateRenderingControlSettings();
            documentsManager.DocumentClosed += (s, e) => UpdateRenderingControlSettings();
            documentsManager.AllDocumentsClosed += (s, e) => UpdateRenderingControlSettings();
        }

        private void UpdateRenderingControlSettings()
        {
            bool visible = GridPlane != null ? GridPlane.Visible : true;
            GridPlane = documentsManager.TotalBoundingBox.IsEmpty ? new GridPlane()
               : new GridPlane(5, documentsManager.TotalBoundingBox.Diagonal * 2, documentsManager.TotalBoundingBox.MinPoint.Z);
            GridPlane.Visible = visible;
        }

        private void DrawOriginCoordinateSystem()
        {
            OpenGLGeometryWrapper.DrawPoint(Point.ZeroPoint, RGB.BlackColor, 20);
            OpenGLGeometryWrapper.DrawLine(
                new Line(Point.ZeroPoint, Point.ZeroPoint + Vector.XAxis * CoordinateSystemAxisLength), RGB.RedColor, 10);
            OpenGLGeometryWrapper.DrawLine(
                new Line(Point.ZeroPoint, Point.ZeroPoint + Vector.YAxis * CoordinateSystemAxisLength), RGB.GreenColor, 10);
            OpenGLGeometryWrapper.DrawLine(
               new Line(Point.ZeroPoint, Point.ZeroPoint + Vector.ZAxis * CoordinateSystemAxisLength), RGB.BlueColor, 10);
        }

        private void DrawEntities()
        {
            documentsManager.GeometryToRender.ForEach(
                g => OpenGLGeometryWrapper.DrawGeometryObject(g, UseGlobalMaterial ? GlobalMaterial : null));
        }

        #endregion Private logic
    }
}
