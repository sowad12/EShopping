using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Utilities
{
    public static class EnumerableUtilities
    {
        public static void ForEach<T>(this IEnumerable<T> _this, Action<T> callback)
        {
            foreach (T _thi in _this)
            {
                callback(_thi);
            }
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> hash = new HashSet<TKey>();
            return source.Where((TSource p) => hash.Add(keySelector(p)));
        }

        public static int Count(this IEnumerable source)
        {
            ICollection collection = source as ICollection;
            int c = 0;
            if (collection != null)
            {
                c = collection.Count;
            }
            else
            {
                IEnumerator e = source.GetEnumerator();
                e.DynamicUsing(delegate
                {
                    while (e.MoveNext())
                    {
                        int num = c;
                        c = num + 1;
                    }
                });
            }

            return c;
        }

        public static List<T> CreateEmptyList<T>(this T type, int? count = null)
        {
            return CreateEmptyList<T>();
        }

        public static List<T> CreateEmptyList<T>(int? count = null)
        {
            return count.HasValue ? new List<T>(count.Value) : new List<T>();
        }

        public static string Join<T>(this IEnumerable<T> items, string separator)
        {
            if (items.Any())
            {
                return items.Select((T i) => i.ToString()).Aggregate((string acc, string next) => acc + separator + next);
            }

            return "";
        }

        public static T Rand<T>(this IEnumerable<T> items)
        {
            IList<T> list = ((!(items is IList<T>)) ? items.ToList() : ((IList<T>)items));
            return list[new Random().Next(list.Count)];
        }

        public static IList<T> Shuffle<T>(this IList<T> array)
        {
            T[] array2 = new T[array.Count];
            array.CopyTo(array2, 0);
            Random random = new Random();
            for (int i = 0; i < array.Count; i++)
            {
                int num = random.Next(i, array.Count);
                if (num != i)
                {
                    T val = array2[i];
                    array2[i] = array2[num];
                    array2[num] = val;
                }
            }

            return array2;
        }

        public static void SafeAddRange<T>(this List<T> list, IEnumerable<T> collection)
        {
            if (collection != null && collection.Count() > 0)
            {
                list.AddRange(collection);
            }
        }
    }
}
