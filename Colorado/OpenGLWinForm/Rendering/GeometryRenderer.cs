using Colorado.Documents;
using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry2D;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.OpenGLWrappers;
using Colorado.OpenGLWinForm.Managers;

namespace Colorado.OpenGLWinForm.Rendering
{
    internal class GeometryRenderer
    {
        #region Private fields

        private readonly DocumentsManager documentsManager;
        private readonly LightsManager lightsManager;

        #endregion Private fields

        #region Constructor

        public GeometryRenderer(DocumentsManager documentsManager, LightsManager lightsManager)
        {
            this.documentsManager = documentsManager;
            this.lightsManager = lightsManager;
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
                OpenGLGeometryWrapper.DrawGeometryObject(geometryObject, lightsManager);
            }
        }

        #endregion Private logic
    }
}
