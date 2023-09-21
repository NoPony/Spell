using Spell.Core.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Spell.Core
{
    public class SpellService
    {
        private readonly TokenIndex _index;

        public SpellService()
        {
            _index = new TokenIndex();

            foreach (string word in new StringReader(Words.words_alpha).Lines())
                _index.Insert(word);
        }

        public IEnumerable<Result> Check(string word)
        {
            IEnumerable<TokenResult> bigramResults = _index.Probe(word);
            
            int maxScore = bigramResults.Any()
                ? bigramResults.Max(i  => i.Score)
                : 0;

            return bigramResults
                .Where(i => i.Score > maxScore - 3)
                .Take(32)
                .Select(i => new
                {
                    i.Score,
                    Value = _index.Document(i.Index),
                })
                .Select(i => new
                {
                    i.Value,
                    TokenScore = i.Score,
                    Distance = word.LevenshteinDistance(i.Value),
                    FirstLetter = word[0] == i.Value[0],
                })
                .Select(i => new Result
                {
                    Value = i.Value,
                    TokenScore = i.TokenScore,
                    Distance = i.Distance,
                    FirstLetter = i.FirstLetter,
                    Score = 0,
                })
                .OrderBy(i => i.Distance)
                .Take(7);
        }
    }
}
