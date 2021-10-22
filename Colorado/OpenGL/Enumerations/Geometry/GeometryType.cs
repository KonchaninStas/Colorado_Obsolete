using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGL.Enumerations.Geometry
{
    internal enum GeometryType
    {
        Point = 0x0000,
        Line = 0x0001,
        LineLoop = 0x0002,
        Triangle = 0x0004,
        TriangleStrip = 0x0005,
        TriangleFan = 0x0006,
        Quad = 0x0007
    }
}
