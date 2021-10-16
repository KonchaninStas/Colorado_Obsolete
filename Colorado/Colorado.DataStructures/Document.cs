using Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures;
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

        }

        public IEnumerable<GeometryObject> Geometries => geometryObjects;

        public void AddGeometryObject(GeometryObject geometryObject)
        {

        }
    }
}
