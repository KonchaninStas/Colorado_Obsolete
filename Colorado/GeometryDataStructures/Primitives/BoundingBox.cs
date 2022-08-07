using System;
using System.Collections.Generic;
using System.Linq;

namespace Colorado.GeometryDataStructures.Primitives
{
    public class BoundingBox
    {
        #region Constructors

        public BoundingBox() : this(Point.ZeroPoint, Point.ZeroPoint) { }

        public BoundingBox(Point maxPoint, Point minPoint)
        {
            Init(maxPoint, minPoint);
        }

        #endregion Constructors

        #region Properties

        public Point MaxPoint { get; private set; }

        public Point MinPoint { get; private set; }

        public bool IsEmpty => MaxPoint.Equals(MinPoint);

        public Point Center { get; private set; }

        public double Diagonal { get; private set; }

        #endregion Properties

        #region Public logic

        public void Add(BoundingBox boundingBox)
        {
            if (IsEmpty)
            {
                Init(boundingBox.MaxPoint, boundingBox.MinPoint);
            }
            else
            {
                Init(GetPointWithMaxValues(new[] { MaxPoint, boundingBox.MaxPoint }),
                    GetPointWithMinValues(new[] { MinPoint, boundingBox.MinPoint }));
            }
        }

        public void ApplyTransform(Transform transform)
        {
            Init(transform.ApplyToPoint(MaxPoint), transform.ApplyToPoint(MinPoint));
        }

        public void ResetToDefault()
        {
            Init(Point.ZeroPoint, Point.ZeroPoint);
        }

        public BoundingBox Clone()
        {
            return new BoundingBox(MaxPoint, MinPoint);
        }

        public static Point GetPointWithMinValues(IEnumerable<Point> points)
        {
            return new Point(GetValuesFromPoints(points, p => p.X).Min(), GetValuesFromPoints(points, p => p.Y).Min(),
               GetValuesFromPoints(points, p => p.Z).Min());
        }

        public static Point GetPointWithMaxValues(IEnumerable<Point> points)
        {
            return new Point(GetValuesFromPoints(points, p => p.X).Max(), GetValuesFromPoints(points, p => p.Y).Max(),
               GetValuesFromPoints(points, p => p.Z).Max());
        }

        #endregion Public logic

        #region Private logic

        private void Init(Point maxPoint, Point minPoint)
        {
            MaxPoint = GetPointWithMaxValues(new[] { maxPoint, minPoint });
            MinPoint = GetPointWithMinValues(new[] { maxPoint, minPoint });

            var diagonalVector = new Vector(minPoint, maxPoint);

            Diagonal = diagonalVector.Length;
            Center = minPoint + diagonalVector.UnitVector() * Diagonal / 2;
        }

        private static IEnumerable<double> GetValuesFromPoints(IEnumerable<Point> points, Func<Point, double> getValueFromPointAction)
        {
            return points.Select(p => getValueFromPointAction(p));
        }

        #endregion Private logic
    }
}
