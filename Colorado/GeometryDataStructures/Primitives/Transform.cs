using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.GeometryDataStructures.Primitives
{
    public class Transform
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
            set
            {
                this[3, 0] = value.X;
                this[3, 1] = value.Y;
                this[3, 2] = value.Z;
            }
        }

        /// <summary>
        /// Scale is calculated as length of basis vector components of this transform.
        /// </summary>
        public Point Scale
        {
            get
            {
                return new Point(
                    new Vector(this[0, 0], this[1, 0], this[2, 0]).Length,
                    new Vector(this[0, 1], this[1, 1], this[2, 1]).Length,
                    new Vector(this[0, 2], this[1, 2], this[2, 2]).Length);
            }
        }

        #endregion Properties

        #region Public methods

        public Transform Identity()
        {
            var identityTransform = new Transform();
            identityTransform.MakeIdentity();

            return identityTransform;
        }

        #region Operator overrides

        public static Transform operator *(Transform left, Transform right)
        {
            var result = new Transform();

            for (int row = 0; row < oneDimentionalSize; row++)
            {
                for (int col = 0; col < oneDimentionalSize; col++)
                {
                    for (int k = 0; k < oneDimentionalSize; k++)
                    {
                        result[row, col] += left[row, k] * right[k, col];
                    }
                }
            }

            return result;
        }

        #endregion Operator overrides

        private void MakeIdentity()
        {
            for (int i = 0; i < oneDimentionalSize; i++)
            {
                this[i, i] = 1; // Set main diagonal to '1'.
            }
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

        public void Translate(Vector vector)
        {
            Translation = Translation + vector;
        }

        public void ScaleTranslation(double value)
        {
            Translation = Translation * value;
        }

        #endregion Public methods
    }
}
