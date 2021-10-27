using Colorado.Common.Helpers;
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

        public Mesh(ICollection<Triangle> triangles)
        {
            Triangles = triangles;
            BoundingBox = GetBoundingBox();

            TrianglesCount = triangles.Count;
            VerticesCount = triangles.Count * triangleVerticesCount;

            VerticesValuesArray = ArrayHelper.MergeArrays(triangles.SelectMany(t => t.VerticesValuesArray).ToArray(),
                triangleVerticesCount);
        }

        public double[] VerticesValuesArray { get; }

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
    }
}
