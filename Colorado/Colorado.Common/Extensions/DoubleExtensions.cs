using System;

namespace Colorado.Common.Extensions
{
    public static class DoubleExtensions
    {
        public static bool IsZero(this double value)
        {
            return EqualsWithTolerance(value, default(double));
        }

        public static bool EqualsWithTolerance(this double firstValue, double secondValue)
        {
            return firstValue.EqualsWithTolerance(secondValue, double.Epsilon);
        }

        public static bool EqualsWithTolerance(this double firstValue, double secondValue, double tolerance)
        {
            return Math.Abs(firstValue - secondValue) <= tolerance;
        }
    }
}
