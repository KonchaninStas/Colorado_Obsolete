using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
using Colorado.GeometryDataStructures.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.DataStructures
{
    public class Document
    {
        private readonly IList<GeometryObject> geometryObjects;

        public Document()
        {
            geometryObjects = new List<GeometryObject>();
            //BoundingBox = new BoundingBox();
        }

        public IEnumerable<GeometryObject> Geometries => geometryObjects;

        public BoundingBox BoundingBox => new BoundingBox(new Point(1000, 1000, 1000), new Point(-1000, -1000, -1000));

        public void AddGeometryObject(GeometryObject geometryObject)
        {
            geometryObjects.Add(geometryObject);
            BoundingBox.Add(geometryObject.BoundingBox);
        }
    }
}
