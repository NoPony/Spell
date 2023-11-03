using Spell.Core.Extensions;
using Spell.Core.Indices.Bigram.BigramSearch;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Spell.Core
{
    public class SpellService
    {
        private readonly HashSet<string> _words;
        private readonly BigramSearchIndex _index;

        public SpellService()
        {
            _words = new HashSet<string>();
            _index = new BigramSearchIndex();

            IEnumerable<string> words = new StringReader(Words.words_alpha)
                .Lines()
                .Select(i => i.ToLowerInvariant());

            foreach (string word in words)
            {
                _words.Add(word);
                _index.Insert(word);
            }
        }

        //public IEnumerable<string> CheckDocument(string document)
        //{

        //}

        public MatchResult CheckWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return new MatchResult
                {
                    Match = false,
                    Suggestions = Enumerable.Empty<Suggestion>()
                };

            string input = word.ToLowerInvariant();

            if (_words.Contains(input))
                return new MatchResult
                {
                    Match = true,
                    Suggestions = Enumerable.Empty<Suggestion>()
                };

            IEnumerable<BigramSearchResult> bigramResults = _index.Probe(input);

            return new MatchResult
            {
                Match = false,
                Suggestions = bigramResults
                .Take(32)
                .Select(i => new
                {
                    i.Score,
                    Value = _index.Word(i.Index),
                })
                .Select(i => new Suggestion
                {
                    Value = i.Value,
                    BigramSearchScore = i.Score,
                    LevenshteinDistance = input.LevenshteinDistance(i.Value),
                    FirstLetterMatch = input[0] == i.Value[0],
                    Confidence = 0,
                })
                .OrderBy(i => i.LevenshteinDistance)
                .Take(7)
            };
        }
    }
}
