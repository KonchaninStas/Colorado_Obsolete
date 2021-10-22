using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.Common.Utilities
{
    public static class MathUtilities
    {
        public static double ConvertRadiansToDegrees(double radians)
        {
            return 180 / Math.PI * radians;
        }

        public static double ConvertDegreesToRadians(double degrees)
        {
            return Math.PI / 180 * degrees;
        }

        public static double Clamp(double value, double min, double max)
        {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
            return value;
        }
    }
}
