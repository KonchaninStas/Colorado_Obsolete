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
        public Mesh(ICollection<Triangle> triangles)
        {
            Triangles = triangles;
            BoundingBox = GetBoundingBox();
        }

        public ICollection<Triangle> Triangles { get; }

        public override GeometryType GeometryType => GeometryType.Mesh;

        public override BoundingBox BoundingBox { get; }

        private BoundingBox GetBoundingBox()
        {
            IEnumerable<Point> points = Triangles.SelectMany(t =>
                new[] { t.FirstVertex.Position, t.SecondVertex.Position, t.ThirdVertex.Position });

            return new BoundingBox(BoundingBox.GetPointWithMaxValues(points), BoundingBox.GetPointWithMinValues(points));
        }

        public Mesh GetTransformed(Transform transform)
        {
            var transformedTriangles = new List<Triangle>(Triangles.Count);

            foreach (Triangle triangle in Triangles)
            {
                transformedTriangles.Add(triangle.GetTransformed(transform));
            }

            return new Mesh(transformedTriangles);
        }
    }
}
