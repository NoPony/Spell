namespace Spell.Core
{
    public class Suggestion
    {
        public string Value { get; set; }
        public int? BigramSearchScore { get; set; }
        public int? LevenshteinDistance { get; set; }
        public bool? FirstLetterMatch { get; set; }

        public float? Confidence { get; set; }
    }
}
