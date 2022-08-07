using System;
using System.Collections.Generic;

namespace Colorado.Common.Extensions
{
    public static class CollectionExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> actionToApply)
        {
            foreach (T item in collection)
            {
                actionToApply.Invoke(item);
            }
        }
    }
}
