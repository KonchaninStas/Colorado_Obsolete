using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.GeometryDataStructures.Primitives
{
    public class BoundingBox
    {
        public BoundingBox(Point maxPoint, Point minPoint)
        {
            MaxPoint = maxPoint;
            MinPoint = minPoint;
        }

        public Point MaxPoint { get; }

        public Point MinPoint { get; }
    }
}
