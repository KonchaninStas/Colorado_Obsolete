using Colorado.DataStructures;
using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry2D;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.OpenGLWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGLWinForm
{
    public class GeometryRenderer
    {
        private readonly Document activeDocument;

        public GeometryRenderer(Document activeDocument)
        {
            this.activeDocument = activeDocument;
        }

        public void DrawSceneGeometry()
        {
            DrawOriginCoordianteSystem();
        }

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
            //OpenGLGeometryWrapper.DrawPoint(Point.ZeroPoint, RGBA.BlueColor, 1);

            //if (PointUnderMouse != null)
            //{
            //    OpenGLGeometryWrapper.DrawPoint(PointUnderMouse, RGBA.RedColor, 10);
            //}


            foreach (GeometryObject geometryObject in activeDocument.Geometries)
            {
                OpenGLGeometryWrapper.DrawGeometryObject(geometryObject);
            }
        }
    }
}
