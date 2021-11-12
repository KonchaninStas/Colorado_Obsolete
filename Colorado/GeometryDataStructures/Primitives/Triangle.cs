using Colorado.Common.Helpers;
using Colorado.GeometryDataStructures.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.GeometryDataStructures.Primitives
{
    public class Triangle
    {
        public Triangle(Vertex firstVertex, Vertex secondVertex, Vertex thirdVertex, Vector normal)
        {
            Color = new RGB(0, 0, 0);
            FirstVertex = firstVertex;
            SecondVertex = secondVertex;
            ThirdVertex = thirdVertex;

            Normal = normal;
            Center = (FirstVertex.Position + SecondVertex.Position + ThirdVertex.Position) / 3;
        }

        public RGB Color { get; }
        public Vertex FirstVertex { get; }

        public Vertex SecondVertex { get; }

        public Vertex ThirdVertex { get; }

        public Vector Normal { get; }

        public Point Center { get; }

        internal Triangle GetTransformed(Transform transform)
        {
            return new Triangle(FirstVertex.GetTransformed(transform), SecondVertex.GetTransformed(transform),
                ThirdVertex.GetTransformed(transform), transform.ApplyToVector(Normal));
        }
    }
}
