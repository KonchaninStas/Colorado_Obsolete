using Colorado.GeometryDataStructures.Colors;
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

            VerticesValuesArray = new[] { StartPoint.X, StartPoint.Y, StartPoint.Z, EndPoint.X, EndPoint.Y, EndPoint.Z };

            var Color = new RGBA(126, 126, 126);
            RGBColorsValuesArray = new double[]
           {
                Color.Red, Color.Green, Color.Blue,
                Color.Red, Color.Green, Color.Blue
           };
        }

        public const int VerticesValuesArrayLength = 6;

        public const int RGBColorsValuesArrayLength = 6;

        public double[] VerticesValuesArray { get; }

        public double[] RGBColorsValuesArray { get; }

        public Point StartPoint { get; }

        public Point EndPoint { get; }

        public Vector Direction { get; }

        public override GeometryType GeometryType => GeometryType.Line;

        public override BoundingBox BoundingBox { get; }
    }
}
