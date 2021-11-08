using Colorado.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.GeometryDataStructures.Primitives
{
    public class Vector : IEquatable<Vector>
    {
        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Length = CalculateLength();
        }

        public Vector(Point startPoint, Point endPoint)
            : this(endPoint.X - startPoint.X, endPoint.Y - startPoint.Y, endPoint.Z - startPoint.Z) { }

        public double X { get; }

        public double Y { get; }

        public double Z { get; }

        public double Length { get; }

        public Vector Inverse => this * -1;

        public bool IsUnitVector => Length.EqualsWithTolerance(1);

        public static Vector ZeroVector => new Vector(0, 0, 0);

        public static Vector XAxis => new Vector(1, 0, 0);

        public static Vector YAxis => new Vector(0, 1, 0);

        public static Vector ZAxis => new Vector(0, 0, 1);

        public bool IsZero => X.IsZero() && Y.IsZero() && Z.IsZero();

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

        public static Vector operator *(double scaleFactor, Vector vector)
        {
            return vector * scaleFactor;
        }

        public static Vector operator /(Vector vector, double scaleFactor)
        {
            return new Vector(vector.X / scaleFactor, vector.Y / scaleFactor, vector.Z / scaleFactor);
        }

        private double CalculateLength()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }



        public Vector UnitVector()
        {
            return new Vector(X / Length, Y / Length, Z / Length);
        }

        public static bool operator ==(Vector firstVector, Vector secondVector)
        {
            return Equals(firstVector, secondVector);
        }

        public static bool operator !=(Vector firstVector, Vector secondVector)
        {
            return !Equals(firstVector, secondVector);
        }

        public Vector ToVector()
        {
            return new Vector(X, Y, Z);
        }

        public bool Equals(Vector other)
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

            return this.Equals((Vector)obj);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }

        public static Vector Reflect(Vector vector, Vector normal)
        {
            double dot = vector.X * normal.X + vector.Y * normal.Y + vector.Z * normal.Z;
            double tempX = normal.X * dot * 2f;
            double tempY = normal.Y * dot * 2f;
            double tempZ = normal.Z * dot * 2f;
            return new Vector(vector.X - tempX, vector.Y - tempY, vector.Z - tempZ);
        }
    }
}
