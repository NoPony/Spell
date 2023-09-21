using Spell.Core.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Spell.Core
{
    public class TokenIndex
    {
        private readonly DiscreteValueList _tokens;
        private readonly Dictionary<int, List<int>> _index;
        private readonly List<string> _documents;
        private int _count;

        public TokenIndex()
        {
            _tokens = new DiscreteValueList();
            _index = new Dictionary<int, List<int>>();
            _documents = new List<string>();
            _count = 0;
        }

        public void Insert(string document)
        {
            _documents.Add(document);

            foreach (string token in document.Bigrams())
            {
                int key = _tokens.Key(token);

                if (_index.TryGetValue(key, out List<int> documentIds))
                    documentIds.Add(_count);

                else
                    _index.Add(key, new List<int> { _count });
            }

            _count++;
        }

        internal IEnumerable<TokenResult> Probe(string query)
        {
            if (query.Length < 2)
                return Enumerable.Empty<TokenResult>();

            return query.Bigrams()
                .Select(i => _tokens.Key(i))
                .Select(i => _index[i])
                .SelectMany(i => i, (k, v) => v)
                .GroupBy(i => i, (k, v) => new TokenResult { Index = k, Score = v.Count() })
                .OrderByDescending(i => i.Score);
        }

        public string Document(int index)
        {
            return _documents[index];
        }
    }
}
