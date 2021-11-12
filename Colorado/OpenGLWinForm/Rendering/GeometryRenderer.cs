using Colorado.Documents;
using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry2D;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.OpenGLWrappers.Geometry;

namespace Colorado.OpenGLWinForm.Rendering
{
    internal class GeometryRenderer
    {
        #region Private fields

        private readonly DocumentsManager documentsManager;

        #endregion Private fields

        #region Constructor

        public GeometryRenderer(DocumentsManager documentsManager)
        {
            this.documentsManager = documentsManager;
        }

        #endregion Constructor 

        #region Public logic

        public void DrawSceneGeometry()
        {
            DrawOriginCoordianteSystem();
            DrawEntities();
        }

        #endregion Public logic

        #region Private logic

        private void DrawOriginCoordianteSystem()
        {
            OpenGLGeometryWrapper.DrawPoint(Point.ZeroPoint, RGB.BlackColor, 1);
            OpenGLGeometryWrapper.DrawLine(
                new Line(Point.ZeroPoint, Point.ZeroPoint + Vector.XAxis * 100), RGB.RedColor);
            OpenGLGeometryWrapper.DrawLine(
                new Line(Point.ZeroPoint, Point.ZeroPoint + Vector.YAxis * 100), RGB.GreenColor);
            OpenGLGeometryWrapper.DrawLine(
               new Line(Point.ZeroPoint, Point.ZeroPoint + Vector.ZAxis * 100), RGB.BlueColor);
        }

        private void DrawEntities()
        {
            documentsManager.GeometryToRender.ForEach(g => OpenGLGeometryWrapper.DrawGeometryObject(g));
        }

        #endregion Private logic
    }
}
