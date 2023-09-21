using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Spell.Core.Extensions
{
    internal static class String_Tokens
    {
        internal static IEnumerable<string> Tokens(this StringReader reader)
        {
            // Preconditions
            if (reader == null)
                yield return null;

            int buffer;

            // 0 | EOF
            buffer = reader.Read();

            if (buffer == -1)
                yield return null;

            // >=0..n : Junk* EOF?
            while (Junk(buffer))
            {
                buffer = reader.Read();
            }

            if (buffer == -1)
                yield return null;

            // Token[0] start
            StringBuilder sb = new StringBuilder();
            string result;

            // >=0..n : (!EOF)*
            while (buffer != -1)
            {
                // It is impossible for buffer to be invalid here
                sb.Append(buffer);

                // Read the next Value
                buffer = reader.Read();

                // If we have Junk, its a Token Delimiter
                //   It is impossible for sb to be invalid here
                if (Junk(buffer))
                {
                    result = sb.ToString();

                    yield return result;

                    sb.Clear();

                    continue;
                }

                // While we still have Junk, consume it
                while (Junk(buffer))
                    buffer = reader.Read();
            }

            if (sb.Length > 0)
            {
                result = sb.ToString();
                sb.Clear();

                yield return result;
            }

            yield return null;
        }

        // This is a WhiteList
        // It only holds good (non-junk) values
        // This should not be used to store 'false' Values (Black/White List)
        //   It wont break, but it will suck
        private static readonly Dictionary<int, bool> _notJunk = Enumerable
            .Union(
                Enumerable.Range('a', 'z' - 'a'),
                Enumerable.Range('A', 'Z' - 'A'))
            .ToDictionary(k => k, v => true);

        private static bool Junk(int value)
        {
            if (value == -1)
                return false;

            // If this Key exists in _notJunk, return its Value (bool)
            if (_notJunk.TryGetValue(value, out bool result))
                return result;

            // If its not in _notJunk, it's Junk
            return true;
        }
    }
}
