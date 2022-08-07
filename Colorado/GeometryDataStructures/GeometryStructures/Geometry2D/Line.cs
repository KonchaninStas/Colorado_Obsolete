using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.GeometryStructures.Enumerations;
using Colorado.GeometryDataStructures.Primitives;

namespace Colorado.GeometryDataStructures.GeometryStructures.Geometry2D
{
    public class Line : GeometryObject
    {
        public Line(Point startPoint, Point endPoint, RGB color)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Direction = new Vector(startPoint, endPoint).UnitVector();
            BoundingBox = new BoundingBox(StartPoint, EndPoint);

            VerticesValuesArray = new[] { StartPoint.X, StartPoint.Y, StartPoint.Z, EndPoint.X, EndPoint.Y, EndPoint.Z };

            RGBColorsValuesArray = new byte[]
            {
                color.Red, color.Green, color.Blue,
                color.Red, color.Green, color.Blue
            };
        }

        public Line(Point startPoint, Point endPoint) : this(startPoint, endPoint, RGB.GridDefaultColor) { }

        public const int VerticesValuesArrayLength = 6;

        public const int RGBColorsValuesArrayLength = 6;

        public double[] VerticesValuesArray { get; }

        public byte[] RGBColorsValuesArray { get; }

        public Point StartPoint { get; }

        public Point EndPoint { get; }

        public Vector Direction { get; }

        public override GeometryType GeometryType => GeometryType.Line;

        public override BoundingBox BoundingBox { get; }
    }
}
