using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.GeometryDataStructures.Primitives
{
    public class Transform : ICloneable
    {
        #region Constants

        private const int oneDimentionalSize = 4;
        private const int matrixSize = 16;

        #endregion Constants

        #region Private fields

        private readonly double[] matrix;

        #endregion Private fields

        #region Constructors

        private Transform()
        {
            matrix = new double[matrixSize];
            MakeIdentity();
        }

        public Transform(double[] matrix) : this()
        {
            this.matrix = matrix;
        }

        private Transform(Vector translation) : this()
        {
            Translation = translation;
        }

        #endregion Constructors

        #region Properties

        public double[] Array => matrix;

        public double this[int index]
        {
            get { return matrix[index]; }
            set { matrix[index] = value; }
        }

        public double this[int row, int column]
        {
            get
            {
                return matrix[row * oneDimentionalSize + column];
            }
            set
            {
                matrix[row * oneDimentionalSize + column] = value;
            }
        }

        /// <summary>
        /// Gets or sets Translation component of this transformation.
        /// </summary>
        public Vector Translation
        {
            get
            {
                return new Vector(
                    this[0, 3],
                    this[1, 3],
                    this[2, 3]);
            }
            private set
            {
                this[0, 3] = value.X;
                this[1, 3] = value.Y;
                this[2, 3] = value.Z;
            }
        }

        /// <summary>
        /// Scale is calculated as length of basis vector components of this transform.
        /// </summary>
        public double Scale
        {
            get
            {
                return Math.Sqrt(this[0, 0] * this[0, 0] + this[0, 1] * this[0, 1] + this[0, 2] * this[0, 2]);
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Creates a matrix for rotating points around the X-axis.
        /// </summary>
        /// <param name="rotationAngleInRadians">The amount, in radians, by which to rotate around the X-axis.</param>
        /// <returns>The rotation matrix.</returns>
        public static Transform CreateRotationX(double rotationAngleInRadians)
        {
            double cos = Math.Cos(rotationAngleInRadians);
            double sin = Math.Sin(rotationAngleInRadians);

            // [  1  0  0  0 ]
            // [  0  c  s  0 ]
            // [  0 -s  c  0 ]
            // [  0  0  0  1 ]

            Transform result = Identity();

            result[1, 1] = cos;
            result[1, 2] = sin;
            result[2, 1] = -sin;
            result[2, 2] = cos;

            return result;
        }

        /// <summary>
        /// Creates a matrix for rotating points around the Y-axis.
        /// </summary>
        /// <param name="rotationAngleInRadians">The amount, in radians, by which to rotate around the Y-axis.</param>
        /// <returns>The rotation matrix.</returns>
        public static Transform CreateRotationY(double rotationAngleInRadians)
        {
            double cos = Math.Cos(rotationAngleInRadians);
            double sin = Math.Sin(rotationAngleInRadians);

            // [  c  0 -s  0 ]
            // [  0  1  0  0 ]
            // [  s  0  c  0 ]
            // [  0  0  0  1 ]

            Transform result = Identity();

            result[0, 0] = cos;
            result[2, 0] = sin;
            result[0, 2] = -sin;
            result[2, 2] = cos;

            return result;
        }

        /// <summary>
        /// Creates a matrix for rotating points around the Z-axis.
        /// </summary>
        /// <param name="rotationAngleInRadians">The amount, in radians, by which to rotate around the Z-axis.</param>
        /// <returns>The rotation matrix.</returns>
        public static Transform CreateRotationZ(double rotationAngleInRadians)
        {
            double cos = Math.Cos(rotationAngleInRadians);
            double sin = Math.Sin(rotationAngleInRadians);

            // [  c  s  0  0 ]
            // [ -s  c  0  0 ]
            // [  0  0  1  0 ]
            // [  0  0  0  1 ]

            Transform result = Identity();

            result[0, 0] = cos;
            result[1, 0] = -sin;
            result[0, 1] = sin;
            result[1, 1] = cos;

            return result;
        }

        /// <summary>
        /// Creates a matrix that rotates around an arbitrary vector.
        /// </summary>
        /// <param name="rotationAxis">The axis to rotate around.</param>
        /// <param name="rotationAngleInRadians">The angle to rotate around the given axis, in radians.</param>
        /// <returns>The rotation matrix.</returns>
        public static Transform CreateFromAxisAngle(Vector rotationAxis, double rotationAngleInRadians)
        {
            // a: angle
            // x, y, z: unit vector for axis.
            //
            // Rotation matrix M can compute by using below equation.
            //
            //        T               T
            //  M = uu + (cos a)( I-uu ) + (sin a)S
            //
            // Where:
            //
            //  u = ( x, y, z )
            //
            //      [  0 -z  y ]
            //  S = [  z  0 -x ]
            //      [ -y  x  0 ]
            //
            //      [ 1 0 0 ]
            //  I = [ 0 1 0 ]
            //      [ 0 0 1 ]
            //
            //
            //     [  xx+cosa*(1-xx)   yx-cosa*yx-sina*z zx-cosa*xz+sina*y ]
            // M = [ xy-cosa*yx+sina*z    yy+cosa(1-yy)  yz-cosa*yz-sina*x ]
            //     [ zx-cosa*zx-sina*y zy-cosa*zy+sina*x   zz+cosa*(1-zz)  ]
            //
            double x = rotationAxis.X;
            double y = rotationAxis.Y;
            double z = rotationAxis.Z;
            double sa = (double)Math.Sin(rotationAngleInRadians);
            double ca = (double)Math.Cos(rotationAngleInRadians);
            double xx = x * x;
            double yy = y * y;
            double zz = z * z;
            double xy = x * y;
            double xz = x * z;
            double yz = y * z;

            Transform result = Identity();

            result[0, 0] = xx + ca * (1.0 - xx);
            result[0, 1] = xy - ca * xy + sa * z;
            result[0, 2] = xz - ca * xz - sa * y;
            result[1, 0] = xy - ca * xy - sa * z;
            result[1, 1] = yy + ca * (1 - yy);
            result[1, 2] = yz - ca * yz + sa * x;
            result[2, 0] = xz - ca * xz + sa * y;
            result[2, 1] = yz - ca * yz - sa * x;
            result[2, 2] = zz + ca * (1 - zz);

            return result;
        }

        public static Transform Identity()
        {
            var identityTransform = new Transform();
            identityTransform.MakeIdentity();

            return identityTransform;
        }

        public static Transform LookAt(Point cameraPosition, Point targetPoint, Vector upVector)
        {
            Vector direction = new Vector(targetPoint, cameraPosition).UnitVector();
            Vector rightVector = upVector.CrossProduct(direction).UnitVector();
            Vector upVectorUpdated = direction.CrossProduct(rightVector).UnitVector();

            Transform rotation = Identity();
            rotation[0, 0] = rightVector.X;
            rotation[1, 0] = rightVector.Y;
            rotation[2, 0] = rightVector.Z;

            rotation[0, 1] = upVectorUpdated.X;
            rotation[1, 1] = upVectorUpdated.Y;
            rotation[2, 1] = upVectorUpdated.Z;

            rotation[0, 2] = -direction.X;
            rotation[1, 2] = -direction.Y;
            rotation[2, 2] = -direction.Z;

            return rotation;
        }

        /// <summary>
        /// Creates a scaling matrix.
        /// </summary>
        /// <param name="xScale">Value to scale by on the X-axis.</param>
        /// <param name="yScale">Value to scale by on the Y-axis.</param>
        /// <param name="zScale">Value to scale by on the Z-axis.</param>
        /// <returns>The scaling matrix.</returns>
        public static Transform CreateScale(double xScale, double yScale, double zScale)
        {
            Transform result = Transform.Identity();

            result[0, 0] = xScale;
            result[1, 1] = yScale;
            result[2, 2] = zScale;

            return result;
        }

        public static Transform CreateScale(double scale)
        {
            Transform result = Transform.Identity();

            result[0, 0] = scale;
            result[1, 1] = scale;
            result[2, 2] = scale;

            return result;
        }

        public static Transform CreateTranslation(Vector translationVector)
        {
            Transform result = Transform.Identity();

            result.Translation = translationVector;

            return result;
        }

        #region Operator overrides

        public static Transform operator *(Transform left, Transform right)
        {
            return left.Multiply(right);
        }

        public static Vector operator *(Transform transform, Vector vector)
        {
            return transform.ApplyToVector(vector);
        }

        public Transform Multiply(Transform anotherOne)
        {
            var result = new Transform();

            for (int row = 0; row < oneDimentionalSize; row++)
            {
                for (int col = 0; col < oneDimentionalSize; col++)
                {
                    for (int k = 0; k < oneDimentionalSize; k++)
                    {
                        result[row, col] += this[row, k] * anotherOne[k, col];
                    }
                }
            }

            return result;
        }

        public Point ApplyToPoint(Point point)
        {
            double x = this[0, 0] * point.X + this[0, 1] * point.Y + this[0, 2] * point.Z + this[0, 3];
            double y = this[1, 0] * point.X + this[1, 1] * point.Y + this[1, 2] * point.Z + this[1, 3];
            double z = this[2, 0] * point.X + this[2, 1] * point.Y + this[2, 2] * point.Z + this[2, 3];
            return new Point(x, y, z);
        }

        public Vector ApplyToVector(Vector vector)
        {
            double x = this[0, 0] * vector.X + this[0, 1] * vector.Y + this[0, 2] * vector.Z;
            double y = this[1, 0] * vector.X + this[1, 1] * vector.Y + this[1, 2] * vector.Z;
            double z = this[2, 0] * vector.X + this[2, 1] * vector.Y + this[2, 2] * vector.Z;
            return new Vector(x, y, z);
        }

        #endregion Operator overrides

        private void MakeIdentity()
        {
            for (int i = 0; i < oneDimentionalSize; i++)
            {
                this[i, i] = 1; // Set main diagonal to '1'.
            }
        }

        public void Translate(Vector vector)
        {
            Translation = Translation + vector;
        }

        public void SetTranslation(Point point)
        {
            Translation = point.ToVector();
        }

        public void ScaleTranslation(double value)
        {
            Translation = Translation * value;
        }

        public Transform Clone()
        {
            var result = new Transform();
            for (int i = 0; i < oneDimentionalSize * oneDimentionalSize; i++)
            {
                result[i] = this[i];
            }

            return result;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public Quaternion ToQuaternion()
        {
            double trace = this[0, 0] + this[1, 1] + this[2, 2];

            if (trace > 0.0)
            {
                double s = (double)Math.Sqrt(trace + 1.0);
                double w = s * 0.5;
                s = 0.5 / s;

                return new Quaternion(
                    (this[1, 2] - this[2, 1]) * s,
                    (this[2, 0] - this[0, 2]) * s,
                    (this[0, 1] - this[1, 0]) * s,
                    w);
            }
            else
            {
                if (this[0, 0] >= this[1, 1] && this[0, 0] >= this[2, 2])
                {
                    double s = (double)Math.Sqrt(1.0f + this[0, 0] - this[1, 1] - this[2, 2]);
                    double invS = 0.5 / s;
                    return new Quaternion(
                        0.5 * s,
                        (this[0, 1] + this[1, 0]) * invS,
                        (this[0, 2] + this[2, 0]) * invS,
                        (this[1, 2] - this[2, 1]) * invS);
                }
                else if (this[1, 1] > this[2, 2])
                {
                    double s = (double)Math.Sqrt(1.0f + this[1, 1] - this[0, 0] - this[2, 2]);
                    double invS = 0.5 / s;

                    return new Quaternion(
                        (this[1, 0] + this[0, 1]) * invS,
                        0.5 * s,
                        (this[2, 1] + this[1, 2]) * invS,
                        (this[2, 0] - this[0, 2]) * invS);
                }
                else
                {
                    double s = (double)Math.Sqrt(1.0f + this[2, 2] - this[0, 0] - this[1, 1]);
                    double invS = 0.5 / s;
                    return new Quaternion(
                        (this[2, 0] + this[0, 2]) * invS,
                        (this[2, 1] + this[1, 2]) * invS,
                        0.5 * s,
                        (this[0, 1] - this[1, 0]) * invS);
                }
            }
        }

        public Transform GetInverted()
        {
            double a = this[0, 0], b = this[0, 1], c = this[0, 2], d = this[0, 3];
            double e = this[1, 0], f = this[1, 1], g = this[1, 2], h = this[1, 3];
            double i = this[2, 0], j = this[2, 1], k = this[2, 2], l = this[2, 3];
            double m = this[3, 0], n = this[3, 1], o = this[3, 2], p = this[3, 3];

            double kp_lo = k * p - l * o;
            double jp_ln = j * p - l * n;
            double jo_kn = j * o - k * n;
            double ip_lm = i * p - l * m;
            double io_km = i * o - k * m;
            double in_jm = i * n - j * m;

            double a11 = +(f * kp_lo - g * jp_ln + h * jo_kn);
            double a12 = -(e * kp_lo - g * ip_lm + h * io_km);
            double a13 = +(e * jp_ln - f * ip_lm + h * in_jm);
            double a14 = -(e * jo_kn - f * io_km + g * in_jm);

            double det = a * a11 + b * a12 + c * a13 + d * a14;

            if (Math.Abs(det) < double.Epsilon)
            {
                return new Transform(new double[]{ double.NaN, double.NaN, double.NaN, double.NaN,
                                       double.NaN, double.NaN, double.NaN, double.NaN,
                                       double.NaN, double.NaN, double.NaN, double.NaN,
                                       double.NaN, double.NaN, double.NaN, double.NaN });
            }

            double invDet = 1.0f / det;

            double M11 = a11 * invDet;
            double M21 = a12 * invDet;
            double M31 = a13 * invDet;
            double M41 = a14 * invDet;

            double M12 = -(b * kp_lo - c * jp_ln + d * jo_kn) * invDet;
            double M22 = +(a * kp_lo - c * ip_lm + d * io_km) * invDet;
            double M32 = -(a * jp_ln - b * ip_lm + d * in_jm) * invDet;
            double M42 = +(a * jo_kn - b * io_km + c * in_jm) * invDet;

            double gp_ho = g * p - h * o;
            double fp_hn = f * p - h * n;
            double fo_gn = f * o - g * n;
            double ep_hm = e * p - h * m;
            double eo_gm = e * o - g * m;
            double en_fm = e * n - f * m;

            double M13 = +(b * gp_ho - c * fp_hn + d * fo_gn) * invDet;
            double M23 = -(a * gp_ho - c * ep_hm + d * eo_gm) * invDet;
            double M33 = +(a * fp_hn - b * ep_hm + d * en_fm) * invDet;
            double M43 = -(a * fo_gn - b * eo_gm + c * en_fm) * invDet;

            double gl_hk = g * l - h * k;
            double fl_hj = f * l - h * j;
            double fk_gj = f * k - g * j;
            double el_hi = e * l - h * i;
            double ek_gi = e * k - g * i;
            double ej_fi = e * j - f * i;

            double M14 = -(b * gl_hk - c * fl_hj + d * fk_gj) * invDet;
            double M24 = +(a * gl_hk - c * el_hi + d * ek_gi) * invDet;
            double M34 = -(a * fl_hj - b * el_hi + d * ej_fi) * invDet;
            double M44 = +(a * fk_gj - b * ek_gi + c * ej_fi) * invDet;

            return new Transform(new double[]
            {
                M11, M12, M13, M14,
                M21, M22, M23, M24,
                M31, M32, M33, M34,
                M41, M42, M43, M44,
            });
        }
        public override string ToString()
        {
            string x = string.Empty;

            for (int i = 0; i < Array.Length; i++)
            {
                x += $" {Array[i]}";
            }
            return x;
        }
        #endregion Public methods
    }
}
