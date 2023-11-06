using Spell.Core.Extensions;

namespace Spell.Test.Extensions
{
    public class String_Bigrams
    {
        [Fact]
        public void EmptyString()
        {
            Assert.Equal(Enumerable.Empty<string>(), "".Bigrams());
        }

        [Fact]
        public void ShortString()
        {
            Assert.Equal(Enumerable.Empty<string>(), "A".Bigrams());
        }

        [Fact]
        public void Normal()
        {
            Assert.Equal(new[] { "AB", "BC", "CD", "DE", "EF" }, "ABCDEF".Bigrams());
        }
    }
}
