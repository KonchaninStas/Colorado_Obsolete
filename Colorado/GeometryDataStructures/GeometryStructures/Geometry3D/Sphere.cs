using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.GeometryStructures.Enumerations;
using Colorado.GeometryDataStructures.Primitives;
using System;
using System.Collections.Generic;

namespace Colorado.GeometryDataStructures.GeometryStructures.Geometry3D
{
    public class Sphere : GeometryObject
    {
        #region Private fields

        private readonly double radiusSphere;
        private readonly Point sphereCenterPoint;
        private readonly int slicesCount;
        private readonly int ringsCount;
        private readonly List<Point> vertices;

        #endregion Private fields

        #region Constructors

        public Sphere(double radiusSphere, Point sphereCenterPoint, int slicesSphere, int ringsSphere)
        {
            vertices = new List<Point>();
            this.radiusSphere = radiusSphere;
            this.sphereCenterPoint = sphereCenterPoint;
            slicesCount = slicesSphere;
            ringsCount = ringsSphere;
        }

        #endregion Constructors

        #region Properties

        public override GeometryType GeometryType => GeometryType.Sphere;

        public override BoundingBox BoundingBox { get; }

        #endregion Properties

        #region Public logic

        public IEnumerable<Triangle> GetTriangle()
        {
            return CreateTriangles(CreateQuadrangles());
        }

        #endregion Public logic

        #region Private logic

        private void GetSphereVertices()
        {
            vertices.Add(new Point(sphereCenterPoint.X, sphereCenterPoint.Y + radiusSphere, sphereCenterPoint.Z));

            for (int i = 0; i < ringsCount - 1; ++i)
            {
                double phi = Math.PI * (i + 1) / ringsCount;
                for (int j = 0; j < slicesCount; ++j)
                {
                    double theta = 2.0 * Math.PI * j / slicesCount;
                    double x = radiusSphere * Math.Sin(phi) * Math.Cos(theta) + sphereCenterPoint.X;
                    double y = radiusSphere * Math.Cos(phi) + sphereCenterPoint.Y;
                    double z = radiusSphere * Math.Sin(phi) * Math.Sin(theta) + sphereCenterPoint.Z;

                    vertices.Add(new Point(x, y, z));
                }
            }
            vertices.Add(new Point(sphereCenterPoint.X, sphereCenterPoint.Y - radiusSphere, sphereCenterPoint.Z));
        }

        private List<Quadrangle> CreateQuadrangles()
        {
            GetSphereVertices();
            List<Quadrangle> quadrangles = new List<Quadrangle>(ringsCount * slicesCount);

            for (int j = 0; j < ringsCount - 2; j++)
            {
                int ringPointIndex0 = j * slicesCount + 1;
                int ringPointIndex1 = (j + 1) * slicesCount + 1;

                for (int i = 0; i < slicesCount; i++)
                {
                    int slicePointIndex0 = ringPointIndex0 + i;
                    int slicePointIndex1 = ringPointIndex0 + (i + 1) % slicesCount;
                    int slicePointIndex2 = ringPointIndex1 + (i + 1) % slicesCount;
                    int slicePointIndex3 = ringPointIndex1 + i;

                    quadrangles.Add(new Quadrangle(vertices[slicePointIndex0], vertices[slicePointIndex1],
                                                   vertices[slicePointIndex2], vertices[slicePointIndex3]));
                }
            }
            return quadrangles;
        }

        private IEnumerable<Triangle> CreateTriangles(List<Quadrangle> quadrangles)
        {
            List<Triangle> triangles = new List<Triangle>(quadrangles.Count * 2 + slicesCount * 2);

            for (int i = 0; i < slicesCount; ++i)
            {
                int slicePointIndex0 = i + 1;
                int slicePointIndex1 = (i + 1) % slicesCount + 1;

                triangles.Add(new Triangle(vertices[0], vertices[slicePointIndex1], vertices[slicePointIndex0]));

                slicePointIndex0 = i + slicesCount * (slicesCount - 2) + 1;
                slicePointIndex1 = (i + 1) % slicesCount + slicesCount * (slicesCount - 2) + 1;

                triangles.Add(new Triangle(vertices[vertices.Count - 1], vertices[slicePointIndex0], vertices[slicePointIndex1]));
            }

            for (int i = 0; i < quadrangles.Count; i++)
            {
                triangles.AddRange(quadrangles[i].GetTrianglesFromQuadrangle());
            }
            return triangles;
        }

        private BoundingBox GetBoundingBox()
        {
            return new BoundingBox(sphereCenterPoint + Vector.XAxis * radiusSphere + Vector.YAxis * radiusSphere + Vector.ZAxis * radiusSphere,
                sphereCenterPoint - Vector.XAxis * radiusSphere - Vector.YAxis * radiusSphere - Vector.ZAxis * radiusSphere);
        }

        #endregion Private logic
    }
}
