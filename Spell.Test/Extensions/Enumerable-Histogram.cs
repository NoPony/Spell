using Spell.Core.Extensions;

namespace Spell.Test.Extensions
{
    public class Enumerable_Histogram
    {
        [Fact]
        public void IntTest()
        {
            var input = new[]
            {
                1, 2, 3,
                1, 2,
                1
            };

            var output = input.Histogram();

            var expected = new[] {
                new KeyValuePair<int, int>(1, 3),
                new KeyValuePair<int, int>(2, 2),
                new KeyValuePair<int, int>(3, 1),
            };

            Assert.Equal(expected, output);
        }

        [Fact]
        public void StringTest()
        {
            var input = new[]
            {
                "One", "Two", "Three",
                "One", "Two",
                "One"
            };

            var output = input.Histogram();

            var expected = new[] {
                new KeyValuePair<string, int>("One", 3),
                new KeyValuePair<string, int>("Two", 2),
                new KeyValuePair<string, int>("Three", 1),
            };

            Assert.Equal(expected, output);
        }

    }
}
