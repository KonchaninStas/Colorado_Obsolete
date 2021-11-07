using Colorado.Common.Collections;
using Colorado.Common.Helpers;
using Colorado.GeometryDataStructures.Colors;
using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.GeometryStructures.Enumerations;
using Colorado.GeometryDataStructures.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.GeometryDataStructures.GeometryStructures.Geometry3D
{
    public class Mesh : GeometryObject
    {
        private const int triangleVerticesCount = 3;
        private const int rgbColorValuesCount = 3;

        private readonly DynamicArray<double> verticesValuesArray;
        private readonly DynamicArray<double> normalsValuesArray;
        private readonly DynamicArray<RGBA> verticesColors;
        private readonly DynamicArray<Vector> verticesNormals;

        public Mesh(IList<Triangle> triangles)
        {
            Triangles = triangles;
            BoundingBox = GetBoundingBox();

            TrianglesCount = triangles.Count;
            VerticesCount = triangles.Count * triangleVerticesCount;

            verticesValuesArray = new DynamicArray<double>(triangles.Count * 9);
            normalsValuesArray = new DynamicArray<double>(triangles.Count * 9);
            verticesColors = new DynamicArray<RGBA>(VerticesCount);
            verticesNormals = new DynamicArray<Vector>(VerticesCount);

            for (int i = 0; i < triangles.Count; i++)
            {
                AddTriangleValues(triangles[i]);
            }
        }

        public double[] VerticesValuesArray => verticesValuesArray.Array;

        public double[] NormalsValuesArray => normalsValuesArray.Array;

        public RGBA[] VerticesColors => verticesColors.Array;

        public Vector[] VerticesNormals => verticesNormals.Array;

        public IEnumerable<Triangle> Triangles { get; }

        public override GeometryType GeometryType => GeometryType.Mesh;

        public override BoundingBox BoundingBox { get; }

        public int VerticesCount { get; }
        public int TrianglesCount { get; }

        private BoundingBox GetBoundingBox()
        {
            IEnumerable<Point> points = Triangles.SelectMany(t =>
                new[] { t.FirstVertex.Position, t.SecondVertex.Position, t.ThirdVertex.Position });

            return new BoundingBox(BoundingBox.GetPointWithMaxValues(points), BoundingBox.GetPointWithMinValues(points));
        }

        public Mesh GetTransformed(Transform transform)
        {
            var transformedTriangles = new List<Triangle>(TrianglesCount);

            foreach (Triangle triangle in Triangles)
            {
                transformedTriangles.Add(triangle.GetTransformed(transform));
            }

            return new Mesh(transformedTriangles);
        }

        private void AddTriangleValues(Triangle triangle)
        {
            for (int i = 0; i < 3; i++)
            {
                verticesColors.Add(triangle.Color);
                verticesNormals.Add(triangle.Normal);

                normalsValuesArray.Add(triangle.Normal.X);
                normalsValuesArray.Add(triangle.Normal.Y);
                normalsValuesArray.Add(triangle.Normal.Z);
            }

            AddVertices(triangle);
        }

        private void AddVertices(Triangle triangle)
        {
            verticesValuesArray.Add(triangle.FirstVertex.Position.X);
            verticesValuesArray.Add(triangle.FirstVertex.Position.Y);
            verticesValuesArray.Add(triangle.FirstVertex.Position.Z);

            verticesValuesArray.Add(triangle.SecondVertex.Position.X);
            verticesValuesArray.Add(triangle.SecondVertex.Position.Y);
            verticesValuesArray.Add(triangle.SecondVertex.Position.Z);

            verticesValuesArray.Add(triangle.ThirdVertex.Position.X);
            verticesValuesArray.Add(triangle.ThirdVertex.Position.Y);
            verticesValuesArray.Add(triangle.ThirdVertex.Position.Z);
        }
    }
}
