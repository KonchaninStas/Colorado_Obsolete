using Colorado.GeometryDataStructures.GeometryStructures.Enumerations;
using Colorado.GeometryDataStructures.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.GeometryDataStructures.GeometryStructures.BaseGeometryStructures
{
    public abstract class GeometryObject
    {
        public abstract GeometryType GeometryType { get; }

        public abstract BoundingBox BoundingBox { get; }
    }
}
