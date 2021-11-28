using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.Geometry2D;
using Colorado.GeometryDataStructures.Primitives;
using Colorado.OpenGL.OpenGLWrappers.Geometry;

namespace Colorado.OpenGLWinForm.Rendering.PrimitivesRenderers
{
    public class CoordinateSystemRenderer
    {
        public CoordinateSystemRenderer()
        {
            DrawCoordinateSystem = true;
            CoordinateSystemAxisLength = 100;
        }

        public bool DrawCoordinateSystem { get; set; }

        public double CoordinateSystemAxisLength { get; set; }

        public void Draw()
        {
            if (DrawCoordinateSystem)
            {
                OpenGLGeometryWrapper.DrawPoint(Point.ZeroPoint, RGB.BlackColor, 20);
                OpenGLGeometryWrapper.DrawLine(
                    new Line(Point.ZeroPoint, Point.ZeroPoint + Vector.XAxis * CoordinateSystemAxisLength), RGB.RedColor, 10);
                OpenGLGeometryWrapper.DrawLine(
                    new Line(Point.ZeroPoint, Point.ZeroPoint + Vector.YAxis * CoordinateSystemAxisLength), RGB.GreenColor, 10);
                OpenGLGeometryWrapper.DrawLine(
                   new Line(Point.ZeroPoint, Point.ZeroPoint + Vector.ZAxis * CoordinateSystemAxisLength), RGB.BlueColor, 10);
            }
        }
    }
}
