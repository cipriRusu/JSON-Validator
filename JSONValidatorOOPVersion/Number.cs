using System;
using System.Collections.Generic;
using System.Text;

namespace JSONValidatorAlternativeVersion
{
    internal class Number : IPattern
    {
        private readonly IPattern pattern;
        public Number()
        {
            var minus = new Character('-');
            var zero = new Character('0');
            var point = new Character('.');
            var digit = new Range('0', '9');
            var digits = new OneOrMore(digit);

            var natural = new Choice(zero, digits);

            var integer = 
                new Sequence(
                    new Optional(minus),
                    natural);

            var fractional = 
                new Sequence(
                    point,
                    digits);

            var exponential =
               new Sequence(
                   new Optional(digits),
                   new Any("Ee"),
                   new Optional(new Any("+-")),
                   digits);

            pattern = 
                new Sequence(
                    integer, 
                    new Optional(fractional),
                    new Optional(exponential));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
