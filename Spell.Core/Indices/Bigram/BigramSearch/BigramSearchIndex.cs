using Spell.Core.Extensions;
using Spell.Core.Indices.Bigram.DiscreteBigram;
using System.Collections.Generic;
using System.Linq;

namespace Spell.Core.Indices.Bigram.BigramSearch
{
    public class BigramSearchIndex
    {
        private readonly DiscreteBigramIndex _discreteBigrams;
        private readonly Dictionary<int, List<int>> _bigramInstances;
        private readonly List<string> _wordList;
        private int _wordCount;

        public BigramSearchIndex()
        {
            _discreteBigrams = new DiscreteBigramIndex();
            _bigramInstances = new Dictionary<int, List<int>>();
            _wordList = new List<string>();
            _wordCount = 0;
        }

        public void Insert(string word)
        {
            _wordList.Add(word);

            foreach (string bigram in word.Bigrams())
            {
                int key = _discreteBigrams.Key(bigram);

                if (_bigramInstances.TryGetValue(key, out List<int> documentIds))
                    documentIds.Add(_wordCount);

                else
                    _bigramInstances.Add(key, new List<int> { _wordCount });
            }

            _wordCount++;
        }

        internal IEnumerable<BigramSearchResult> Probe(string query)
        {
            if (query.Length < 2)
                return Enumerable.Empty<BigramSearchResult>();

            return query.Bigrams()
                .Select(i => _discreteBigrams.Key(i))
                .Select(i => _bigramInstances[i])
                .SelectMany(i => i, (k, v) => v)
                .GroupBy(i => i, (k, v) => new BigramSearchResult { Index = k, Score = v.Count() })
                .OrderByDescending(i => i.Score);
        }

        public string Word(int index)
        {
            return _wordList[index];
        }
    }
}
