using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.GeometryDataStructures.GeometryStructures.Geometry3D
{
    public class Sphere : GeometryObject
    {
        public Sphere(Point centerPoint, double radius)
        {
            Center = centerPoint;
            Radius = radius;
        }

        public Point Center { get; }

        public double Radius { get; }

        public override BoundingBox BoundingBox { get; }

        private BoundingBox GetBoundingBox()
        {
            return new BoundingBox(Center + Vector.XAxis * Radius + Vector.YAxis * Radius + Vector.ZAxis * Radius,
                Center - Vector.XAxis * Radius - Vector.YAxis * Radius - Vector.ZAxis * Radius);
        }
    }
}
