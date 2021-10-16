using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.GeometryDataStructures.Primitives
{
    public class BoundingBox
    {
        public BoundingBox()
        {
            MaxPoint = Point.ZeroPoint;
            MinPoint = Point.ZeroPoint;
            Diagonal = 0;
            Center = Point.ZeroPoint;
        }

        public BoundingBox(Point maxPoint, Point minPoint)
        {
            if (maxPoint > minPoint)
            {
                MaxPoint = maxPoint;
                MinPoint = minPoint;
            }
            else
            {
                MaxPoint = minPoint;
                MinPoint = maxPoint;
            }

            var diagonalVector = new Vector(minPoint, maxPoint);

            Diagonal = diagonalVector.Length;
            Center = minPoint + diagonalVector.UnitVector() * Diagonal / 2;
        }

        public Point MaxPoint { get; private set; }

        public Point MinPoint { get; private set; }

        public bool IsEmpty => MaxPoint.Equals(MinPoint);

        public Point Center { get; }

        public double Diagonal { get; }

        public void Add(BoundingBox boundingBox)
        {
            if (boundingBox.MaxPoint > MaxPoint)
            {
                MaxPoint = boundingBox.MaxPoint;
            }

            if (boundingBox.MinPoint > MinPoint)
            {
                MinPoint = boundingBox.MinPoint;
            }
        }
    }
}
