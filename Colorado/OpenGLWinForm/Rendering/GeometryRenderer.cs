using Colorado.Documents;
using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry2D;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.OpenGLWrappers;

namespace Colorado.OpenGLWinForm.Rendering
{
    public class GeometryRenderer
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

        #region Properties

        #endregion Properties

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
            OpenGLGeometryWrapper.DrawPoint(Point.ZeroPoint, RGBA.BlackColor, 1);
            OpenGLGeometryWrapper.DrawLine(
                new Line(Point.ZeroPoint, Point.ZeroPoint + Vector.XAxis * 100), RGBA.RedColor);
            OpenGLGeometryWrapper.DrawLine(
                new Line(Point.ZeroPoint, Point.ZeroPoint + Vector.YAxis * 100), RGBA.GreenColor);
            OpenGLGeometryWrapper.DrawLine(
               new Line(Point.ZeroPoint, Point.ZeroPoint + Vector.ZAxis * 100), RGBA.BlueColor);
        }

        private void DrawEntities()
        {
            foreach (GeometryObject geometryObject in documentsManager.GeometryToRender)
            {
                OpenGLGeometryWrapper.DrawGeometryObject(geometryObject);
            }
        }

        #endregion Private logic
    }
}
