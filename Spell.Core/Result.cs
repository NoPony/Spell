namespace Spell.Core
{
    public class Result
    {
        public string Value { get; set; }
        public int? TokenScore { get; set; }
        public int? Distance { get; set; }
        public bool? FirstLetter { get; set; }

        public float? Score { get; set; }
    }
}
