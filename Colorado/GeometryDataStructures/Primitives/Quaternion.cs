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
        public Quaternion(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
            Axis = new Vector(x, y, z);
        }

        public Quaternion(Vector axis, double w) : this(axis.X, axis.Y, axis.Z, w) { }

        public double X { get; }

        public double Y { get; }

        public double Z { get; }

        public Vector Axis { get; }

        /// <summary>
        /// Specifies the rotation component of the Quaternion.
        /// </summary>
        public double W { get; }

        public static Quaternion Identity
        {
            get { return new Quaternion(0, 0, 0, 1); }
        }

        public bool IsIdentity
        {
            get { return X.IsZero() && Y.IsZero() && Z.IsZero() && W.EqualsWithTolerance(1); }
        }
    }
}
