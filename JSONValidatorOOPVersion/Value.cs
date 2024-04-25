using System;
using System.Collections.Generic;
using System.Text;

namespace JSONValidatorAlternativeVersion
{
    public class Value : IPattern
    {
        public readonly IPattern pattern;
        public Value()
        {
            var whitespace = new Many(new Any(" \n\r\t"));
            var openbracket = new Sequence(whitespace, new Character('['), whitespace);
            var closedbracket = new Sequence(whitespace, new Character(']'), whitespace);
            var openAccolade = new Sequence(whitespace, new Character('{'), whitespace);
            var closedAccolade = new Sequence(whitespace, new Character('}'), whitespace);
            var separator = new Sequence(whitespace, new Character(':'), whitespace);
            var comma = new Sequence(new Character(','), whitespace);

            var value =
                new Choice(
                    new String(),
                    new Number(),
                    new Text("true"),
                    new Text("false"),
                    new Text("null"));

            var array = new Sequence(
                openbracket, new List(value, comma), closedbracket);

            var elements =
                new List(
                    new Sequence(
                        whitespace,
                        new String(),
                        separator,
                        value),
                    comma);

            var obj = new Sequence(
                openAccolade,
                elements,
                closedAccolade);

            value.Add(array);
            value.Add(obj);

            this.pattern = new Sequence(whitespace, value, whitespace);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
