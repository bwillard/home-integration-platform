using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZWaveDeviceBridge
{
    public static class Extension
    {
        public static bool Same<T>(this IEnumerable<T> list1, IEnumerable<T> list2) where T : IEquatable<T>
        {
            if (null == list1)
            {
                return null == list2;
            }

            if (list2 == null)
            {
                return false;
            }

            if (list1.Count() != list2.Count())
            {
                return false;
            }

            IEnumerator<T> list2e = list2.GetEnumerator();
            list2e.MoveNext();

            foreach (T item in list1)
            {
                if (!item.Equals(list2e.Current))
                {
                    return false;
                }
                list2e.MoveNext();

            }

            return true;
        }

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (T item in list)
            {
                action(item);
            }
        }
    }
}
