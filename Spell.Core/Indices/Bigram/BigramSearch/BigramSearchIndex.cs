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
                int key = _discreteBigrams.ProbeSert(bigram);

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

            try
            {
                IEnumerable<string> bigrams = GetBigrams(query);
                IEnumerable<int> bigramIndices = GetOrdinals(bigrams).ToList();
                IEnumerable<List<int>> bigramInstances = GetInstances(bigramIndices);
                IEnumerable<int> flatBigramInstances = FlattenInstances(bigramInstances);
                IEnumerable<BigramSearchResult> instancesPerWord = GroupByWords(flatBigramInstances);
                IEnumerable<BigramSearchResult> scorePositive = FilterUnmatched(instancesPerWord);
                IOrderedEnumerable<BigramSearchResult> bigramSearchResults = SortResult(scorePositive);

                return bigramSearchResults;
            }

            catch (KeyNotFoundException)
            {
                return Enumerable.Empty<BigramSearchResult>();
            }
        }

        public string Word(int index)
        {
            return _wordList[index];
        }

        private static IEnumerable<string> GetBigrams(string query)
        {
            return query.Bigrams();
        }

        private IEnumerable<int> GetOrdinals(IEnumerable<string> bigrams)
        {
            return bigrams.Select(i => _discreteBigrams.Probe(i));
        }

        private IEnumerable<List<int>> GetInstances(IEnumerable<int> bigramIndecies)
        {
            return bigramIndecies.Select(i => _bigramInstances[i]);
        }

        private static IEnumerable<int> FlattenInstances(IEnumerable<List<int>> bigramInstances)
        {
            return bigramInstances.SelectMany(i => i, (k, v) => v);
        }

        private static IEnumerable<BigramSearchResult> GroupByWords(IEnumerable<int> flatBigramInstances)
        {
            return flatBigramInstances.GroupBy(i => i, (k, v) => new BigramSearchResult { Index = k, Score = v.Count() });
        }

        private static IEnumerable<BigramSearchResult> FilterUnmatched(IEnumerable<BigramSearchResult> instancesPerWord)
        {
            return instancesPerWord.Where(i => i.Score > 0);
        }

        private static IOrderedEnumerable<BigramSearchResult> SortResult(IEnumerable<BigramSearchResult> scorePositive)
        {
            return scorePositive.OrderByDescending(i => i.Score);
        }
    }
}
