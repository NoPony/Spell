using System.Collections.Generic;

namespace Spell.Core
{
    public class MatchResult
    {
        public bool Match { get; set; }
        public IEnumerable<Suggestion> Suggestions { get; set; }
    }
}
