using System.Collections.Generic;
using System.Linq;

namespace Spell.Core.Extensions
{
    internal static class String_Bigrams
    {
        internal static IEnumerable<string> Bigrams(this string value)
        {
            if (value.Length < 2)
                return Enumerable.Empty<string>();

            return Enumerable
                .Range(0, value.Length - 1)
                .Select(i => $"{value[i]}{value[i + 1]}");
        }
    }
}
