using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.GeometryDataStructures.Primitives
{
    public class Vertex
    {

        public Vertex(double x, double y, double z) : this(new Point(x, y, z)) { }

        public Vertex(Point point)
        {
            Position = point;
            VerticesValuesArray = new[] { Position.X, Position.Y, Position.Z };
        }

        public Point Position { get; }

        public double[] VerticesValuesArray { get; }

        internal Vertex GetTransformed(Transform transform)
        {
            return new Vertex(transform.ApplyToPoint(Position));
        }
    }
}
