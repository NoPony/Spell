using System.Collections.Generic;
using System.Linq;

namespace Spell.Core.Extensions
{
    /// <summary>
    /// Currently not used, probably never will be
    /// </summary>
    internal static class Enumerable_Histogram
    {
        internal static IEnumerable<KeyValuePair<T, int>> Histogram<T>(this IEnumerable<T> source) 
        {
            Dictionary<T, int> buffer = new Dictionary<T, int>();

            foreach (T item in source)
            {
                if (buffer.TryGetValue(item, out int count))
                    buffer[item] = ++count;

                else
                    buffer.Add(item, 1);
            }

            return buffer.AsEnumerable();
        }
    }
}
