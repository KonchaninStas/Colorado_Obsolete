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
        }

        public Transform(double[] matrix) : this()
        {
            this.matrix = matrix;
        }

        public Transform(Vector translation) : this()
        {
            MakeIdentity();
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
                    this[3, 0],
                    this[3, 1],
                    this[3, 2]);
            }
            private set
            {
                this[3, 0] = value.X;
                this[3, 1] = value.Y;
                this[3, 2] = value.Z;
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
            return point + Translation * Scale;
        }

        public Vector ApplyToVector(Vector vector)
        {
            double x = this[0, 0] * vector.X + this[0, 1] * vector.Y + this[0, 2] * vector.Z + this[0, 3];
            double y = this[1, 0] * vector.X + this[1, 1] * vector.Y + this[1, 2] * vector.Z + this[1, 3];
            double z = this[2, 0] * vector.X + this[2, 1] * vector.Y + this[2, 2] * vector.Z + this[2, 3];
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
                else if (this[1,1] > this[2,2])
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

        #endregion Public methods
    }
}
