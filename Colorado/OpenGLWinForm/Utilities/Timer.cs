using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGLWinForm.Utilities
{
    public static class Timer
    {
        private readonly static Stopwatch _Stopwatch = Stopwatch.StartNew();

        /// <summary>
        /// Gets the system time in seconds (double-precision)
        /// </summary>
        /// <value>The system time in seconds.</value>
        static public double SysTime
        {
            get
            {
                return ((double)_Stopwatch.ElapsedMilliseconds) / 1000.0;
            }
        }

    }
}
