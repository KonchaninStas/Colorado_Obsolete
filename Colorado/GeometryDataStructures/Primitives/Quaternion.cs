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

        public Quaternion() : this(Vector.ZeroVector, 0)
        {

        }

        public Vector RotationAxis { get; }

        public double RotationAngleInRadians { get; }

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
    }
}
