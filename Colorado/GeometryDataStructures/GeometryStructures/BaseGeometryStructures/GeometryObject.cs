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
        public abstract BoundingBox BoundingBox { get; }
    }
}
