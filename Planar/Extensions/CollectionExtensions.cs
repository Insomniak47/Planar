using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planar.Extensions
{
    public static class CollectionExtensions
    {
        public static void SafeAdd<T,U>(this Dictionary<T,List<U>> dict, T key, U value )
        {
            if (dict.TryGetValue(key, out var val) && val != null)
                dict[key].Add(value);
            else
                dict[key] = new List<U> { value };
        }

        public static void SafeAddRange<T,U>(this Dictionary<T,List<U>> dict, T key, IEnumerable<U> values)
        {
            if (dict.TryGetValue(key, out var val) && val != null)
                dict[key].AddRange(values);
            else
                dict[key] = new List<U>(values);
        }

        public static IEnumerable<U> Flatten<T,U>(this Dictionary<T,List<U>> dict)
        {
            var values = new List<U>();

            foreach (var item in dict)
            {
                values.AddRange(item.Value);
            }

            return values;
        }
    }
}
