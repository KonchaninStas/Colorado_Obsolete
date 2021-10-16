using Colorado.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.GeometryDataStructures.Primitives
{
    public class Vector
    {
        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Length = CalculateLength();
            UnitVector = GetUnitVector();
        }

        public Vector(Point startPoint, Point endPoint)
            : this(endPoint.X - startPoint.X, endPoint.Y - startPoint.Y, endPoint.Z - startPoint.Z) { }

        public double X { get; }

        public double Y { get; }

        public double Z { get; }

        public double Length { get; }

        public Vector UnitVector { get; }

        public bool IsUnitVector => Length.EqualsWithTolerance(1);

        public static Vector ZeroVector => new Vector(0, 0, 0);

        public static Vector XAxis => new Vector(1, 0, 0);

        public static Vector YAxis => new Vector(0, 1, 0);

        public static Vector ZAxis => new Vector(0, 0.0f, 1);

        public Vector GetPerpendicularVector()
        {
            if (IsParallel(XAxis))
            {
                return CrossProduct(YAxis);
            }
            else
            {
                return CrossProduct(XAxis);
            }
        }

        public double AngleToVector(Vector anotherVector)
        {
            return (float)Math.Acos(this.CosToVector(anotherVector));
        }

        public bool IsPerpendicular(Vector anotherVector)
        {
            return DotProduct(anotherVector).IsZero();
        }

        public bool IsParallel(Vector anotherVector)
        {
            return DotProduct(anotherVector).EqualsWithTolerance(Math.PI / 2);
        }

        public double SinToVector(Vector anotherVector)
        {
            return (float)Math.Sqrt(1 - Math.Pow(this.CosToVector(anotherVector), 2));
        }

        public double CosToVector(Vector anotherVector)
        {
            return DotProduct(anotherVector) / (Length * anotherVector.Length);
        }

        public double DotProduct(Vector anotherVector)
        {
            return X * anotherVector.X + Y * anotherVector.Y + Z * anotherVector.Z;
        }

        public Vector CrossProduct(Vector anotherVector)
        {
            double x = Y * anotherVector.Z - anotherVector.Y * Z;
            double y = (X * anotherVector.Z - anotherVector.X * Z) * -1;
            double z = X * anotherVector.Y - anotherVector.X * Y;

            return new Vector(x, y, z);
        }

        public static Vector operator -(Vector left, Vector right)
        {
            return new Vector(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Vector operator +(Vector left, Vector right)
        {
            return new Vector(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static Vector operator *(Vector vector, double scaleFactor)
        {
            return new Vector(vector.X * scaleFactor, vector.Y * scaleFactor, vector.Z * scaleFactor);
        }

        private double CalculateLength()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        private Vector GetUnitVector()
        {
            return new Vector(X / Length, Y / Length, Z / Length);
        }
    }
}
