using Colorado.Common.Extensions;
using Colorado.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.GeometryDataStructures.Primitives
{
    public class Quaternion
    {
        private readonly double x;
        private readonly double y;
        private readonly double z;
        private readonly double w;
        private readonly Vector axis;

        public Quaternion(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
            axis = new Vector(x, y, z);
        }

        public Vector Axis => axis;

        /// <summary>
        /// Gets the angle of the quaternion.
        /// </summary>
        /// <value>The quaternion's angle.</value>
        public double AngleInRadians
        {
            get
            {
                double length = axis.Length * 2;
                if (length.IsZero())
                    return 0.0;

                return (float)(2.0 * Math.Acos(MathUtilities.Clamp(w, -1f, 1f)));
            }
        }

        private Quaternion(Vector axis, double w) : this(axis.X, axis.Y, axis.Z, w) { }

        public EulerAngles GetEulerAngles()
        {
            // roll (x-axis rotation)
            double sinr_cosp = 2 * (w * x +y * z);
            double cosr_cosp = 1 - 2 * (x * x + y * y);
            double roll = Math.Atan2(sinr_cosp, cosr_cosp);

            // pitch (y-axis rotation)
            double pitch = 0;
            double sinp = 2 * (w * y - z * x);
            if (Math.Abs(sinp) >= 1)
                pitch = (Math.PI / 2).CopySign(sinp); // use 90 degrees if out of range
            else
                pitch = Math.Asin(sinp);

            // yaw (z-axis rotation)
            double siny_cosp = 2 * (w * z + x * y);
            double cosy_cosp = 1 - 2 * (y * y + z * z);
            double yaw = Math.Atan2(siny_cosp, cosy_cosp);
            return new EulerAngles(roll, pitch, yaw);
        }

        public static Quaternion Create(Vector rotationAxis, double rotationAngleInDegrees)
        {
            if (rotationAxis.IsZero)
            {
                return Identity;
            }
            double rotationAngleInRadians = MathUtilities.ConvertDegreesToRadians(rotationAngleInDegrees);
            rotationAngleInRadians *= 0.5;
            rotationAxis = rotationAxis.UnitVector();
            rotationAxis = rotationAxis * Math.Sin(rotationAngleInRadians);

            return new Quaternion(rotationAxis.X, rotationAxis.Y, rotationAxis.Z, Math.Cos(rotationAngleInRadians)).GetNormalized();
        }

        public static Quaternion LookRotation(Vector forward, Vector up)
        {
            forward = forward.UnitVector();
            Vector right = up.CrossProduct(forward).UnitVector();
            up = forward.CrossProduct(right);
            double m00 = right.X;
            double m01 = right.Y;
            double m02 = right.Z;
            double m10 = up.X;
            double m11 = up.Y;
            double m12 = up.Z;
            double m20 = forward.X;
            double m21 = forward.Y;
            double m22 = forward.Z;


            double num8 = m00 + m11 + m22;
            if (num8 > 0f)
            {
                double num = Math.Sqrt(num8 + 1f);
                double w = num * 0.5f;
                num = 0.5f / num;
                return new Quaternion((m12 - m21) * num, (m20 - m02) * num, (m01 - m10) * num, w);
            }
            if ((m00 >= m11) && (m00 >= m22))
            {
                double num7 = Math.Sqrt(1f + m00 - m11 - m22);
                double num4 = 0.5f / num7;
                return new Quaternion(0.5f * num7, (m01 + m10) * num4, (m02 + m20) * num4, (m12 - m21) * num4);
            }
            if (m11 > m22)
            {
                double num6 = (double)Math.Sqrt(1f + m11 - m00 - m22);
                double num3 = 0.5f / num6;
                return new Quaternion((m10 + m01) * num3, 0.5f * num6, (m21 + m12) * num3, (m20 - m02) * num3);
            }

            double num5 = Math.Sqrt(1f + m22 - m00 - m11);
            double num2 = 0.5f / num5;
            return new Quaternion((m20 + m02) * num2, (m21 + m12) * num2, 0.5f * num5, (m01 - m10) * num2);
        }

        public Quaternion Multiply(Quaternion rhs)
        {
            return new Quaternion(
               w * rhs.x + x * rhs.w + y * rhs.z - z * rhs.y,
               w * rhs.y + y * rhs.w + z * rhs.x - x * rhs.z,
               w * rhs.z + z * rhs.w + x * rhs.y - y * rhs.x,
               w * rhs.w - x * rhs.x - y * rhs.y - z * rhs.z);
        }

        public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
        {
            return lhs.Multiply(rhs);
        }

        public static Vector operator *(Quaternion quaternion, Vector vector)
        {
            return quaternion.ApplyToVector(vector);
        }

        public Vector ApplyToVector(Vector vector)
        {
            double x = this.x * 2F;
            double y = this.y * 2F;
            double z = this.z * 2F;
            double xx = this.x * x;
            double yy = this.y * y;
            double zz = this.z * z;
            double xy = this.x * y;
            double xz = this.x * z;
            double yz = this.y * z;
            double wx = this.w * x;
            double wy = this.w * y;
            double wz = this.w * z;

            return new Vector(
                (1F - (yy + zz)) * vector.X + (xy - wz) * vector.Y + (xz + wy) * vector.Z,
                (xy + wz) * vector.X + (1F - (xx + zz)) * vector.Y + (yz - wx) * vector.Z,
                (xz - wy) * vector.X + (yz + wx) * vector.Y + (1F - (xx + yy)) * vector.Z);
        }

        public Quaternion GetInversed()
        {
            double lengthSq = GetLengthSquarted();
            if (lengthSq != 0.0)
            {
                double i = 1.0 / lengthSq;
                return new Quaternion(axis * -i, w * i);
            }
            return this;
        }

        private double GetLength()
        {
            return Math.Sqrt(GetLengthSquarted());
        }

        private double GetLengthSquarted()
        {
            return x * x + y * y + z * z + w * w;
        }

        private Quaternion GetNormalized()
        {
            double scale = 1.0 / GetLength();
            return new Quaternion(axis * scale, w * scale);
        }

        public static Quaternion Identity
        {
            get { return new Quaternion(0, 0, 0, 1); }
        }

        public bool IsIdentity
        {
            get { return x.IsZero() && y.IsZero() && z.IsZero() && w.EqualsWithTolerance(1); }
        }
    }
}
