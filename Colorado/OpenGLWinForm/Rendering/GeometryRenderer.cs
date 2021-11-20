using Colorado.Documents;
using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry2D;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.OpenGLWrappers.Geometry;
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
        }

        #endregion Constructor 

        #region Properties

        public bool UseGlobalMaterial { get; set; }

        public Material GlobalMaterial { get; set; }

        #endregion Properties

        #region Public logic

        public void DrawGeometryPrimitives()
        {
            DrawOriginCoordianteSystem();
        }

        public void DrawSceneGeometry()
        {
            DrawEntities();
        }

        #endregion Public logic

        #region Private logic

        private void DrawOriginCoordianteSystem()
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
