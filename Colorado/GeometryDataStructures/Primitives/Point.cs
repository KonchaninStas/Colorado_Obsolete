using Colorado.Common.Extensions;
using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.GeometryStructures.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.GeometryDataStructures.Primitives
{
    public class Point : IEquatable<Point>
    {
        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            FloatArray = new float[] { (float)X, (float)Y, (float)Z, 1 };
        }

        public double X { get; }

        public double Y { get; }

        public double Z { get; }
        public Point Inverse => this * -1;

        public double LargestAbsoluteComponent
        {
            get
            {
                return new double[]
                {
                    Math.Abs(X),
                    Math.Abs(Y),
                    Math.Abs(Z)
                }.Max();
            }
        }

        public float[] FloatArray { get; }

        public static Point MaxPoint => new Point(double.MaxValue, double.MaxValue, double.MaxValue);

        public static Point MinPoint => new Point(double.MinValue, double.MinValue, double.MinValue);

        public static Point ZeroPoint => new Point(default(double), default(double), default(double));

        public bool IsZero => X.EqualsWithTolerance(0) && Y.EqualsWithTolerance(0) && Z.EqualsWithTolerance(0);

        public double DistanceTo(Point secondPoint)
        {
            return Math.Abs((this - secondPoint).Length);
        }

        public static Vector operator -(Point left, Point right)
        {
            return new Vector(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Point operator -(Point point, Vector vector)
        {
            return new Point(point.X - vector.X, point.Y - vector.Y, point.Z - vector.Z);
        }

        public static Point operator +(Point left, Point right)
        {
            return new Point(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static Point operator +(Point point, Vector vector)
        {
            return new Point(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z);
        }

        public static Point operator *(Point point, double scaleFactor)
        {
            return new Point(point.X * scaleFactor, point.Y * scaleFactor, point.Z * scaleFactor);
        }

        public static Point operator /(Point point, double scaleFactor)
        {
            return new Point(point.X / scaleFactor, point.Y / scaleFactor, point.Z / scaleFactor);
        }

        public static bool operator ==(Point firstPoint, Point secondPoint)
        {
            return Equals(firstPoint, secondPoint);
        }

        public static bool operator !=(Point firstPoint, Point secondPoint)
        {
            return !Equals(firstPoint, secondPoint);
        }

        public Vector ToVector()
        {
            return new Vector(X, Y, Z);
        }

        public bool Equals(Point other)
        {
            if (other == null)
            {
                return false;
            }

            return Math.Abs(X - other.X).IsZero() && Math.Abs(Y - other.Y).IsZero() && Math.Abs(Z - other.Z).IsZero();
        }

        public override string ToString()
        {
            return $" X: {X} ; Y: {Y} ; Z: {Z} ;";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }
    }
}
