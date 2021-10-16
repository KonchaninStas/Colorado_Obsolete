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
    }
}
