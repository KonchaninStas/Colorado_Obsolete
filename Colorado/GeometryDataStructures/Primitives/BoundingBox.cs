using System;
using System.Collections.Generic;
using System.Linq;

namespace Colorado.GeometryDataStructures.Primitives
{
    public class BoundingBox
    {
        public BoundingBox() : this(Point.ZeroPoint, Point.ZeroPoint) { }

        public BoundingBox(Point maxPoint, Point minPoint)
        {
            MaxPoint = GetPointWithMaxValues(new[] { maxPoint, minPoint });
            MinPoint = GetPointWithMinValues(new[] { maxPoint, minPoint });

            var diagonalVector = new Vector(minPoint, maxPoint);

            Diagonal = diagonalVector.Length;
            Center = minPoint + diagonalVector.UnitVector() * Diagonal / 2;
        }

        public Point MaxPoint { get; }

        public Point MinPoint { get; }

        public bool IsEmpty => MaxPoint.Equals(MinPoint);

        public Point Center { get; }

        public double Diagonal { get; }

        public BoundingBox Add(BoundingBox boundingBox)
        {
            return new BoundingBox(GetPointWithMaxValues(new[] { MaxPoint, boundingBox.MaxPoint }), 
                GetPointWithMinValues(new[] { MinPoint, boundingBox.MinPoint }));
        }

        internal static Point GetPointWithMinValues(IEnumerable<Point> points)
        {
            return new Point(GetValuesFromPoints(points, p => p.X).Min(), GetValuesFromPoints(points, p => p.Y).Min(),
               GetValuesFromPoints(points, p => p.Z).Min());
        }

        internal static Point GetPointWithMaxValues(IEnumerable<Point> points)
        {
            return new Point(GetValuesFromPoints(points, p => p.X).Max(), GetValuesFromPoints(points, p => p.Y).Max(),
               GetValuesFromPoints(points, p => p.Z).Max());
        }

        private static IEnumerable<double> GetValuesFromPoints(IEnumerable<Point> points, Func<Point, double> getValueFromPointAction)
        {
            return points.Select(p => getValueFromPointAction(p));
        }

        public static BoundingBox operator *(BoundingBox boundingBox, double scaleFactor)
        {
            return new BoundingBox(boundingBox.MaxPoint * scaleFactor, boundingBox.MinPoint * scaleFactor);
        }
    }
}
