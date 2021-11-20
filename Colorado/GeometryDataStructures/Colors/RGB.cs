using System;
using System.Windows.Media;

namespace Colorado.GeometryDataStructures.Colors
{
    [Serializable]
    public class RGB
    {
        public RGB() { }

        public RGB(Color color) :
            this(color.R, color.G, color.B)
        {
        }

        public RGB(float red, float green, float blue) :
            this((byte)(red * byte.MaxValue), (byte)(green * byte.MaxValue), (byte)(blue * byte.MaxValue))
        { }

        public RGB(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Intensity = 100;
        }

        public byte Red { get; set; }

        public byte Green { get; set; }

        public byte Blue { get; set; }

        public int Intensity { get; set; }

        public static RGB BlackColor => new RGB(0, 0, 0);

        public static RGB WhiteColor => new RGB(255, 255, 255);

        public static RGB RedColor => new RGB(255, 0, 0);

        public static RGB GreenColor => new RGB(0, 255, 0);

        public static RGB BlueColor => new RGB(0, 0, 255);

        public Color ToColor()
        {
            return Color.FromRgb(Red, Green, Blue);
        }

        public float[] ToFloat3Array()
        {
            return new[] { Red / (float)byte.MaxValue, Green / (float)byte.MaxValue, Blue / (float)byte.MaxValue };
        }

        public float[] ToFloat4Array()
        {
            return new[] { Red / (float)byte.MaxValue, Green / (float)byte.MaxValue, Blue / (float)byte.MaxValue, 1 };
        }

        public RGB GetCopy()
        {
            return new RGB(Red, Green, Blue);
        }
    }
}
