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
            //for (int i = 0; i < arraysToMerge.Count; i++)
            //{
            //    for (int y = 0; y < arraysToMerge[i].Length; y++)
            //    {
            //        mergedArrays[i * arrayLength + y] = arraysToMerge[i][y];
            //    }
            //}

            //for (int i = 0; i < arraysToMerge.Count; i++)
            //{
            //    for (int y = 0; y < arraysToMerge[i].Length; y++)
            //    {
            //        mergedArrays[i * arrayLength + y] = arraysToMerge[i][y];
            //    }
            //}

            return mergedArrays.ToArray();
        }
    }
}
