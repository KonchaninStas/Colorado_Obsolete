using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.GeometryDataStructures.Primitives
{
    public class Point
    {
        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; }

        public double Y { get; }

        public double Z { get; }

        public static Point operator -(Point left, Point right)
        {
            return new Point(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
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
    }
}
