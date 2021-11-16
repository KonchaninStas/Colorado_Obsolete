using System.Windows.Media;

namespace Colorado.Common.Extensions
{
    public static class ColorExtensions
    {
        public static double DistanceTo(this Color source, Color target)
        {
            System.Drawing.Color c1 = source.ToDrawingColour();
            System.Drawing.Color c2 = target.ToDrawingColour();

            double hue = c1.GetHue() - c2.GetHue();
            double saturation = c1.GetSaturation() - c2.GetSaturation();
            double brightness = c1.GetBrightness() - c2.GetBrightness();

            return (hue * hue) + (saturation * saturation) + (brightness * brightness);
        }

        public static System.Drawing.Color ToDrawingColour(this Color source)
        {
            return System.Drawing.Color.FromArgb((int)source.R, (int)source.G, (int)source.B);
        }
    }
}
