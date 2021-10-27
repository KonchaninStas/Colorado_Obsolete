using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.Common.Helpers
{
    public static class ArrayHelper
    {
        public static T[] MergeArrays<T>(T[][] arraysToMerge, int arraySize)
        {
            T[] mergedArrays = new T[arraysToMerge.Length * arraySize];

            for (int i = 0; i < arraysToMerge.Length; i++)
            {
                for (int y = 0; y < arraySize; y++)
                {
                    mergedArrays[i + y] = arraysToMerge[i][y];
                }
            }

            return mergedArrays;
        }
    }
}
