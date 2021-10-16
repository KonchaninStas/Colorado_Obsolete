using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.GeometryDataStructures.GeometryStructures.Geometry2D
{
    public class Circle : GeometryObject
    {
        public Circle(Point centerPoint, double radius, Vector normal)
        {
            Center = centerPoint;
            Radius = radius;
            Normal = normal.UnitVector();

            BoundingBox = GetBoundingBox();
        }

        public Point Center { get; }

        public double Radius { get; }

        public Vector Normal { get; }

        public override BoundingBox BoundingBox { get; }

        private BoundingBox GetBoundingBox()
        {
            Vector xAxis = Normal.GetPerpendicularVector().UnitVector() * Radius;
            Vector yAxis = Normal.CrossProduct(xAxis).UnitVector() * Radius;

            return new BoundingBox(Center + xAxis + yAxis, Center - xAxis - yAxis);
        }
    }
}
