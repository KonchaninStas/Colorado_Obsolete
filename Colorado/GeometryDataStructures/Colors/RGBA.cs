using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.GeometryDataStructures.Colors
{
    public class RGBA
    {
        public RGBA()
        {
            Alpha = byte.MaxValue;
        }

        public RGBA(byte red, byte green, byte blue) : this()
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public byte Red { get; }

        public byte Green { get; }

        public byte Blue { get; }

        public byte Alpha { get; }

        public static RGBA RedColor => new RGBA(byte.MaxValue, 0, 0);

        public float[] ToFloat3Array()
        {
            return new[] { Red / (float)byte.MaxValue, Green / (float)byte.MaxValue, Blue / (float)byte.MaxValue };
        }
    }
}
