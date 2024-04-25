using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace JSONValidatorAlternativeVersion
{
    class String : IPattern
    {
        private readonly IPattern pattern;
        public String()
        {
            var quote = new Character('"');
            var backslash = new Character('\\');
            var escapeChars = "\"\\\\/bfnrt";
            var digits = new Range('0', '9');

            var hex = new Choice(
                new Range('a', 'f'),
                new Range('A', 'F'),
                digits);

            var hexadecimal = new Sequence(
                new Character('u'),
                hex, hex, hex, hex);

            var escape = new Choice(
                new Any(escapeChars), 
                hexadecimal);

            var character = new Choice(
                new Range('\u0020', '\uFFFF', "\"\\"),
                new Sequence(backslash,escape));

            var characters = new Many(character);

            pattern = new Sequence(quote, characters, quote);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
