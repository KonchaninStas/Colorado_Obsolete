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
            Alpha = 1;
        }

        public RGBA(double red, double green, double blue) : this()
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public double Red { get; }

        public double Green { get; }

        public double Blue { get; }

        public double Alpha { get; }

        public static RGBA BlackColor => new RGBA(0, 0, 0);

        public static RGBA RedColor => new RGBA(1, 0, 0);

        public static RGBA GreenColor => new RGBA(0, 1, 0);

        public static RGBA BlueColor => new RGBA(0, 0, 1);

        public float[] ToFloat3Array()
        {
            return new[] { (float)Red, (float)Green, (float)Blue };
        }

        public static RGBA operator *(RGBA color, double scaleFactor)
        {
            return new RGBA(color.Red * scaleFactor, color.Green * scaleFactor,
                color.Blue * scaleFactor);
        }

        public static RGBA operator +(RGBA color, RGBA anotherColor)
        {
            return new RGBA(color.Red + anotherColor.Red, color.Green + anotherColor.Green,
                color.Blue + anotherColor.Blue);
        }

        public static RGBA operator *(RGBA color, RGBA anotherColor)
        {
            return new RGBA(color.Red * anotherColor.Red, color.Green * anotherColor.Green, color.Blue * anotherColor.Blue);
        }
    }
}
