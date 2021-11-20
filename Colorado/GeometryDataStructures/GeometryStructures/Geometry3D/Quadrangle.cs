using Colorado.GeometryDataStructures.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.GeometryDataStructures.GeometryStructures.Geometry3D
{
    internal class Quadrangle
    {
        #region Constructors

        public Quadrangle(Point firstVertex, Point secondVertex, Point thirdVertex, Point fourthVertex)
        {
            FirstVertex = new Vertex(firstVertex);
            SecondVertex = new Vertex(secondVertex);
            ThirdVertex = new Vertex(thirdVertex);
            FourthVertex = new Vertex(fourthVertex);
        }

        #endregion Constructors

        #region Properties

        public Vertex FirstVertex { get; }

        public Vertex SecondVertex { get; }

        public Vertex ThirdVertex { get; }

        public Vertex FourthVertex { get; }

        #endregion Properties

        #region Public logic

        public Triangle[] GetTrianglesFromQuadrangle()
        {
            return new Triangle[] { new Triangle(FirstVertex, SecondVertex, FourthVertex),
                new Triangle(FourthVertex, SecondVertex, ThirdVertex)};
        }

        #endregion Public logic
    }
}
