using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.GeometryStructures.Enumerations;
using Colorado.GeometryDataStructures.Primitives;

namespace Colorado.GeometryDataStructures.GeometryStructures.Geometry2D
{
    public class Line : GeometryObject
    {
        public Line(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Direction = new Vector(startPoint, endPoint).UnitVector();
            BoundingBox = new BoundingBox(StartPoint, EndPoint);
        }

        public Point StartPoint { get; }

        public Point EndPoint { get; }

        public Vector Direction { get; }

        public override GeometryType GeometryType => GeometryType.Line;

        public override BoundingBox BoundingBox { get; }
    }
}
