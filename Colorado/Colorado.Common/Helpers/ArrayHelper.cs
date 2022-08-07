using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.Common.Helpers
{
    public static class ArrayHelper
    {
        public static T[] MergeArrays<T>(IEnumerable<IEnumerable<T>> arraysToMerge)
        {
            List<T> mergedArrays = new List<T>();

            foreach (IEnumerable<T> array in arraysToMerge)
            {
                foreach (T value in array)
                {
                    mergedArrays.Add(value);
                }
            }

            return mergedArrays.ToArray();
        }
    }
}
