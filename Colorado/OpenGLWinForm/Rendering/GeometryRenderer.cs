using Colorado.Documents;
using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry2D;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.OpenGLWrappers.Geometry;
using Colorado.OpenGLWinForm.Rendering.RenderableObjects;
using Colorado.OpenGLWinForm.RenderingControlStructures;

namespace Colorado.OpenGLWinForm.Rendering
{
    public class GeometryRenderer
    {
        #region Private fields

        private readonly DocumentsManager documentsManager;
        private readonly ViewCamera viewCamera;

        #endregion Private fields

        #region Constructor

        public GeometryRenderer(DocumentsManager documentsManager, ViewCamera viewCamera)
        {
            this.documentsManager = documentsManager;
            this.viewCamera = viewCamera;
            GlobalMaterial = Material.Default;
            DrawCoordinateSystem = true;
            SubscribeToEvents();
            UpdateRenderingControlSettings();
        }

        #endregion Constructor 

        #region Properties

        public GridPlane GridPlane { get; private set; }

        public bool UseGlobalMaterial { get; set; }

        public Material GlobalMaterial { get; set; }

        public bool DrawCoordinateSystem { get; set; }

        #endregion Properties

        #region Public logic

        public void DrawGeometryPrimitives()
        {
            GridPlane.Draw();
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
            GridPlane = documentsManager.TotalBoundingBox.IsEmpty ? new GridPlane()
               : new GridPlane(5, documentsManager.TotalBoundingBox.Diagonal * 5, documentsManager.TotalBoundingBox.MinPoint.Z);
        }

        private void DrawOriginCoordinateSystem()
        {
            OpenGLGeometryWrapper.DrawPoint(Point.ZeroPoint, RGB.BlackColor, 2000 * (float)viewCamera.ViewCameraTransform.Scale);
            OpenGLGeometryWrapper.DrawLine(
                new Line(Point.ZeroPoint, Point.ZeroPoint + Vector.XAxis * 100), RGB.RedColor, 1000 * (float)viewCamera.ViewCameraTransform.Scale);
            OpenGLGeometryWrapper.DrawLine(
                new Line(Point.ZeroPoint, Point.ZeroPoint + Vector.YAxis * 100), RGB.GreenColor, 1000 * (float)viewCamera.ViewCameraTransform.Scale);
            OpenGLGeometryWrapper.DrawLine(
               new Line(Point.ZeroPoint, Point.ZeroPoint + Vector.ZAxis * 100), RGB.BlueColor, 1000 * (float)viewCamera.ViewCameraTransform.Scale);
        }

        private void DrawEntities()
        {
            documentsManager.GeometryToRender.ForEach(
                g => OpenGLGeometryWrapper.DrawGeometryObject(g, UseGlobalMaterial ? GlobalMaterial : null));
        }

        #endregion Private logic
    }
}
