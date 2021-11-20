﻿namespace Colorado.GeometryDataStructures.Primitives
{
    public class Triangle
    {
        public Triangle(Vertex firstVertex, Vertex secondVertex, Vertex thirdVertex, Vector normal)
        {
            FirstVertex = firstVertex;
            SecondVertex = secondVertex;
            ThirdVertex = thirdVertex;

            Normal = normal;
            Center = (FirstVertex.Position + SecondVertex.Position + ThirdVertex.Position) / 3;
        }

        public Triangle(Point firstVertex, Point secondVertex, Point thirdVertex)
        {
            FirstVertex = new Vertex(firstVertex);
            SecondVertex = new Vertex(secondVertex);
            ThirdVertex = new Vertex(thirdVertex);

            Normal = GetNormalVector();
            Center = (FirstVertex.Position + SecondVertex.Position + ThirdVertex.Position) / 3;
        }

        public Triangle(Vertex firstVertex, Vertex secondVertex, Vertex thirdVertex)
        {
            FirstVertex = firstVertex;
            SecondVertex = secondVertex;
            ThirdVertex = thirdVertex;

            Normal = GetNormalVector();
            Center = (FirstVertex.Position + SecondVertex.Position + ThirdVertex.Position) / 3;
        }

        public Vertex FirstVertex { get; }

        public Vertex SecondVertex { get; }

        public Vertex ThirdVertex { get; }

        public Vector Normal { get; }

        public Point Center { get; }

        public Triangle GetTransformed(Transform transform)
        {
            return new Triangle(FirstVertex.GetTransformed(transform), SecondVertex.GetTransformed(transform),
                ThirdVertex.GetTransformed(transform), transform.ApplyToVector(Normal));
        }

        private Vector GetNormalVector()
        {
            Vector firstVector = new Vector(SecondVertex.Position, FirstVertex.Position);
            Vector secondVector = new Vector(ThirdVertex.Position, SecondVertex.Position);
            Vector normal = firstVector.CrossProduct(secondVector);
            return normal.UnitVector();
        }
    }
}
