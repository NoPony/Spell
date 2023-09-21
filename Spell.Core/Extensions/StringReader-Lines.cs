using System.Collections.Generic;
using System.IO;

namespace Spell.Core.Extensions
{
    internal static class StringReader_Lines
    {
        internal static IEnumerable<string> Lines(this StringReader reader)
        {
            string line;

            while ((line = reader.ReadLine()) != null)
                yield return line;
        }
    }
}
