using System;
using System.Collections.Generic;

namespace Spell.Core.Indices.Bigram.DiscreteBigram
{
    internal class DiscreteBigramIndex
    {
        private readonly Dictionary<string, int> _index;
        private readonly List<string> _values;
        private int _count;

        internal DiscreteBigramIndex()
        {
            _index = new Dictionary<string, int>();
            _values = new List<string>();
            _count = 0;
        }

        internal int Key(string value)
        {
            if (_index.TryGetValue(value, out var index))
                return index;

            _index.Add(value, _count);
            _values.Add(value);

            return _count++;
        }

        internal string Value(int key)
        {
            if (key > _count)
                throw new ArgumentOutOfRangeException(nameof(key));

            return _values[key];
        }
    }
}
