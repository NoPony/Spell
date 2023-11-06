using Spell.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spell.Test.Extensions
{
    public class String_LevenshteinDistance
    {
        [Fact]
        public void Empty0()
        {
            Assert.Equal(0, "".LevenshteinDistance(""));
        }

        [Fact]
        public void Empty1()
        {
            Assert.Equal(1, "".LevenshteinDistance("A"));
        }

        [Fact]
        public void Empty6()
        {
            Assert.Equal(6, "".LevenshteinDistance("ABCDEF"));
        }

        [Fact]
        public void InverseEmpty0()
        {
            Assert.Equal(1, "".LevenshteinDistance("A"));
        }

        [Fact]
        public void InverseEmpty1()
        {
            Assert.Equal(1, "".LevenshteinDistance("A"));
        }

        [Fact]
        public void InverseEmpty6()
        {
            Assert.Equal(6, "".LevenshteinDistance("ABCDEF"));
        }

        [Fact]
        public void Match0()
        {
            Assert.Equal(0, "A".LevenshteinDistance("A"));
        }

        [Fact]
        public void Match1()
        {
            Assert.Equal(1, "A".LevenshteinDistance("AB"));
            Assert.Equal(1, "AB".LevenshteinDistance("A"));
            Assert.Equal(1, "ABC".LevenshteinDistance("AC"));
            Assert.Equal(1, "ABC".LevenshteinDistance("AxC"));
            Assert.Equal(1, "ABCDEF".LevenshteinDistance("xBCDEF"));
            Assert.Equal(1, "ABCDEF".LevenshteinDistance("ABCDEx"));
        }

        [Fact]
        public void Match3()
        {
            Assert.Equal(3, "ABCDEF".LevenshteinDistance("BCE"));
        }
    }
}
