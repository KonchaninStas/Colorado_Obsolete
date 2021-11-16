using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Colorado.GeometryDataStructures.Colors
{
    public class RGB
    {
        public RGB(byte red, byte green, byte blue) :
            this(red / (float)byte.MaxValue, green / (float)byte.MaxValue, blue / (float)byte.MaxValue)
        {
        }

        public RGB(float red, float green, float blue)
        {
            Red = red;
            Green = green;
            Blue = blue;

        }

        public RGB(double red, double green, double blue) : this((float)red, (float)green, (float)blue)
        {
        }

        public float Red { get; }

        public float Green { get; }

        public float Blue { get; }

        public static RGB BlackColor => new RGB(0, 0, 0);

        public static RGB WhiteColor => new RGB(1, 1, 1);

        public static RGB RedColor => new RGB(1, 0, 0);

        public static RGB GreenColor => new RGB(0, 1, 0);

        public static RGB BlueColor => new RGB(0, 0, 1);

        public Color ToColor()
        {
            return Color.FromRgb((byte)(Red * byte.MaxValue), (byte)(Green * byte.MaxValue), (byte)(Blue * byte.MaxValue));
        }

        public float[] ToFloat3Array()
        {
            return new[] { Red, Green, Blue };
        }

        public float[] ToFloat4Array()
        {
            return new[] { Red, Green, Blue, 1 };
        }

        public static RGB operator *(RGB color, double scaleFactor)
        {
            return new RGB(color.Red * scaleFactor, color.Green * scaleFactor,
                color.Blue * scaleFactor);
        }

        public static RGB operator +(RGB color, RGB anotherColor)
        {
            return new RGB(color.Red + anotherColor.Red, color.Green + anotherColor.Green,
                color.Blue + anotherColor.Blue);
        }

        public static RGB operator *(RGB color, RGB anotherColor)
        {
            return new RGB(color.Red * anotherColor.Red, color.Green * anotherColor.Green, color.Blue * anotherColor.Blue);
        }
    }
}
