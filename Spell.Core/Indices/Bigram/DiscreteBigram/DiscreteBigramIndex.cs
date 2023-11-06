using System;
using System.Collections.Generic;

namespace Spell.Core.Indices.Bigram.DiscreteBigram
{
    internal class DiscreteBigramIndex
    {
        private readonly Dictionary<string, int> _index;
        private int _count;

        internal DiscreteBigramIndex()
        {
            _index = new Dictionary<string, int>();
            _count = 0;
        }

        internal int ProbeSert(string value)
        {
            if (_index.TryGetValue(value, out var index))
                return index;

            _index.Add(value, _count);

            return _count++;
        }

        internal int Probe(string value)
        {
            if (_index.TryGetValue(value, out var index))
                return index;

            throw new KeyNotFoundException(value);
        }
    }
}
