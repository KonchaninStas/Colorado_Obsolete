using Colorado.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.GeometryDataStructures.Primitives
{
    public class Quaternion
    {
        public Quaternion(Vector axis, double angleInRadians)
        {
            RotationAxis = axis;
            RotationAngleInRadians = angleInRadians;
        }
        public Quaternion(double x, double y, double z, double angleInRadians) : this(new Vector(x, y, z), angleInRadians)
        { }

        public Quaternion() : this(Vector.ZeroVector, 0)
        {

        }

        public Vector RotationAxis { get; }

        public double RotationAngleInRadians { get; }

        public bool IsZero => RotationAxis.IsZero && RotationAngleInRadians.IsZero();

        public static Vector operator *(Quaternion rotation, Vector vector)
        {
            double num = rotation.RotationAxis.X * 2f;
            double num2 = rotation.RotationAxis.Y * 2f;
            double num3 = rotation.RotationAxis.Z * 2f;
            double num4 = rotation.RotationAxis.X * num;
            double num5 = rotation.RotationAxis.Y * num2;
            double num6 = rotation.RotationAxis.Z * num3;
            double num7 = rotation.RotationAxis.X * num2;
            double num8 = rotation.RotationAxis.X * num3;
            double num9 = rotation.RotationAxis.Y * num3;
            double num10 = rotation.RotationAngleInRadians * num;
            double num11 = rotation.RotationAngleInRadians * num2;
            double num12 = rotation.RotationAngleInRadians * num3;

            return new Vector(
                (1f - (num5 + num6)) * vector.X + (num7 - num12) * vector.Y + (num8 + num11) * vector.Z,
                (num7 + num12) * vector.X + (1f - (num4 + num6)) * vector.Y + (num9 - num10) * vector.Z,
                (num8 - num11) * vector.X + (num9 + num10) * vector.Y + (1f - (num4 + num5)) * vector.Z);
        }

        public static Quaternion operator *(Quaternion left, Quaternion right)
        {
            return left.Multiply(right);
        }

        public Quaternion Multiply(Quaternion right)
        {
            return Multiply(this, right);
        }


        private static Quaternion Multiply(Quaternion lhs, Quaternion rhs)
        {
            if (lhs.IsZero)
            {
                return rhs;
            }
            else if (rhs.IsZero)
            {
                return lhs;
            }
            else
            {
                return new Quaternion(
               lhs.RotationAngleInRadians * rhs.RotationAxis.X + lhs.RotationAxis.X * rhs.RotationAngleInRadians + lhs.RotationAxis.Y * rhs.RotationAxis.Z - lhs.RotationAxis.Z * rhs.RotationAxis.Y,
               lhs.RotationAngleInRadians * rhs.RotationAxis.Y + lhs.RotationAxis.Y * rhs.RotationAngleInRadians + lhs.RotationAxis.Z * rhs.RotationAxis.X - lhs.RotationAxis.X * rhs.RotationAxis.Z,
               lhs.RotationAngleInRadians * rhs.RotationAxis.Z + lhs.RotationAxis.Z * rhs.RotationAngleInRadians + lhs.RotationAxis.X * rhs.RotationAxis.Y - lhs.RotationAxis.Y * rhs.RotationAxis.X,
               lhs.RotationAngleInRadians * rhs.RotationAngleInRadians - lhs.RotationAxis.X * rhs.RotationAxis.X - lhs.RotationAxis.Y * rhs.RotationAxis.Y - lhs.RotationAxis.Z * rhs.RotationAxis.Z);

            }

        }
    }
}
