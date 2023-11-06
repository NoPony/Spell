using Spell.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spell.Test.Extensions
{
    public class StringReader_Lines
    {
        [Fact]
        public void CleanInput()
        {
            StringReader sr = new("abc\r\ndef\nghi");

            string[] expected =
            {
                "abc",
                "def",
                "ghi"
            };

            Assert.Equal(expected, sr.Lines());
        }

        [Fact]
        public void LeadingEmpty()
        {
            StringReader sr = new("\r\nabc\r\ndef\nghi");

            string[] expected =
            {
                "abc",
                "def",
                "ghi"
            };

            Assert.Equal(expected, sr.Lines());
        }

        [Fact]
        public void TrailingEmpty()
        {
            StringReader sr = new("abc\r\ndef\nghi\r\n");

            string[] expected =
            {
                "abc",
                "def",
                "ghi"
            };

            Assert.Equal(expected, sr.Lines());
        }

        [Fact]
        public void MiddleEmpty()
        {
            StringReader sr = new("abc\r\n\r\ndef\n\nghi");

            string[] expected =
            {
                "abc",
                "def",
                "ghi"
            };


            Assert.Equal(expected, sr.Lines());
        }
    }
}
