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
            StringReader sr = new StringReader("abc\r\ndef\nghi");

            string[] expected =
            {
                "abc",
                "def",
                "ghi"
            };
        }

        [Fact]
        public void LeadingEmpty()
        {
            StringReader sr = new StringReader("\r\nabc\r\ndef\nghi");

            string[] expected =
            {
                "abc",
                "def",
                "ghi"
            };
        }

        [Fact]
        public void TrailingEmpty()
        {
            StringReader sr = new StringReader("abc\r\ndef\nghi\r\n");

            string[] expected =
            {
                "abc",
                "def",
                "ghi"
            };
        }

        [Fact]
        public void MiddleEmpty()
        {
            StringReader sr = new StringReader("abc\r\n\r\nndef\n\nghi");

            string[] expected =
            {
                "abc",
                "def",
                "ghi"
            };
        }
    }
}
