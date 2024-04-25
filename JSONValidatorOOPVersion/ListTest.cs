using Xunit;

namespace JSONValidatorAlternativeVersion
{
    public class ListTest
    {
        [Fact]
        public void MatchReturnsTrueForValidListValueWithEmptyStringInput()
        {
            var listTestObject = new List(new Range('0', '9'), 
                new Character(','));
            Assert.True(listTestObject.Match("").Success());
        }

        [Fact]
        public void ReturnTextReturnsEmptyStringForEmptyStringInput()
        {
            var listTestObject = new List(new Range('0', '9'), 
                new Character(','));
            Assert.Equal("", listTestObject.Match("").RemainingText());
        }

        [Fact]
        public void MatchReturnsTrueForValidListValueWithStringInputNoPattern()
        {
            var listTestObject = new List(new Range('0', '9'), 
                new Character(','));
            Assert.True(listTestObject.Match("abc").Success());
        }

        [Fact]
        public void RemainingTextReturnsProperStringValueForValidListValuesWithNoStringInput()
        {
            var listTestObject = new List(new Range('0', '9'), 
                new Character(','));
            Assert.Equal("abc", listTestObject.Match("abc").RemainingText());
        }

        [Fact]
        public void MatchingReturnsTrueForValidListValueWithSingleCharacterInPattern()
        {
            var listTestObject = new List(new Range('0', '9'), 
                new Character(','));
            Assert.True(listTestObject.Match("1a").Success());
        }

        [Fact]
        public void ReturnTextReturnsValidStringOutputForSingleCharacterInPattern()
        {
            var listTestObject = new List(new Range('0', '9'), 
                new Character(','));
            Assert.Equal("a", listTestObject.Match("1a").RemainingText());
        }

        [Fact]
        public void MatchingReturnsTrueForValidSequenceInNumericList()
        {
            var listTestObject = new List(new Range('0', '9'), 
                new Character(','));
            Assert.True(listTestObject.Match("1,2,3").Success());
        }

        [Fact]
        public void ReturnTextReturnsValidStringOutputForValidCompleteSequenceInPattern()
        {
            var listTestObject = new List(new Range('0', '9'), 
                new Character(','));
            Assert.Equal("", listTestObject.Match("1,2,3").RemainingText());
        }

        [Fact]
        public void MatchingReturnsTrueForValidCompleteSequenceAndTrailingValues()
        {
            var listTestObject = new List(new Range('0', '9'), 
                new Character(','));
            Assert.True(listTestObject.Match("1,2,3,").Success());
        }

        [Fact]
        public void ReturnTextReturnsValidTrailingValueFromStringWithValidSequence()
        {
            var listTestObject = new List(new Range('0', '9'), 
                new Character(','));
            Assert.Equal(",", listTestObject.Match("1,2,3,").RemainingText());
        }

        [Fact]
        public void MatchReturnsTrueForThreeSeparatedElementsSequence()
        {
            var digits = new OneOrMore(new Range('0', '9'));
            var whitespace = new Many(new Any(" \r\n\t"));
            var separator = new Sequence(whitespace, new Character(';'), 
                whitespace);

            var listTextObject = new List(digits, separator);

            Assert.True(listTextObject.Match("abc").Success());
        }

        [Fact]
        public void ReturnTextReturnsValidOutputTextSequenceFromElementsOnlyString()
        {
            var digits = new OneOrMore(new Range('0', '9'));
            var whitespace = new Many(new Any(" \r\n\t"));
            var separator = new Sequence(whitespace, new Character(';'), 
                whitespace);

            var listTextObject = new List(digits, separator);

            Assert.Equal("abc", listTextObject.Match("abc").RemainingText());
        }

        [Fact]
        public void MatchReturnsValidForSingleElementAndEscpaceSequenceInString()
        {
            var digits = new OneOrMore(new Range('0', '9'));
            var whitespace = new Many(new Any(" \r\n\t"));
            var separator = new Sequence(whitespace, new Character(';'), 
                whitespace);

            var listTextObject = new List(digits, separator);

            Assert.True(listTextObject.Match("1 \n;").Success());
        }

        [Fact]
        public void ReturnTextReturnsEscapeValueFromValidStringSequence()
        {
            var digits = new OneOrMore(new Range('0', '9'));
            var whitespace = new Many(new Any(" \r\n\t"));
            var separator = new Sequence(whitespace, new Character(';'), 
                whitespace);

            var listTextObject = new List(digits, separator);

            Assert.Equal(" \n;", listTextObject.Match("1 \n;").RemainingText());
        }

        [Fact]
        public void MatchReturnsTrueForComplexElementAndEscapeSequenceString()
        {
            var digits = new OneOrMore(new Range('0', '9'));
            var whitespace = new Many(new Any(" \r\n\t"));
            var separator = new Sequence(whitespace, new Character(';'), 
                whitespace);

            var listTextObject = new List(digits, separator);

            Assert.True(listTextObject.Match("1; 22  ;\n 333 \t; 22").Success());
        }

        [Fact]
        public void ReturnTextReturnsEmptyStringComplexElementAndEscapeSequenceString()
        {
            var digits = new OneOrMore(new Range('0', '9'));
            var whitespace = new Many(new Any(" \r\n\t"));
            var separator = new Sequence(whitespace, new Character(';'), 
                whitespace);

            var listTextObject = new List(digits, separator);

            Assert.Equal("", listTextObject.Match("1; 22  ;\n 333 \t; 22").RemainingText());
        }

        [Fact]
        public void MatchReturnsTrueForStringComprisedOnlyOfElementsNoSeparators()
        {
            var digits = new OneOrMore(new Range('0', '9'));
            var whitespace = new Many(new Any(" \r\n\t"));
            var separator = new Sequence(whitespace, new Character(';'), 
                whitespace);

            var listTextObject = new List(digits, separator);

            Assert.True(listTextObject.Match("01234").Success());
        }

        [Fact]
        public void ReturnTextReturnsValidStringOutputForElementsNoSeparatorsOnly()
        {
            var digits = new OneOrMore(new Range('0', '9'));
            var whitespace = new Many(new Any(" \r\n\t"));
            var separator = new Sequence(whitespace, new Character(';'), 
                whitespace);

            var listTextObject = new List(digits, separator);

            Assert.Equal("", listTextObject.Match("01234").RemainingText());
        }

        [Fact]
        public void MatchReturnsValidOutputForListStartingWithSeparator()
        {
            var list = new List(
                new Range('0', '9'), 
                new Character(','));

            Assert.Equal(",3,4", list.Match(",3,4").RemainingText());
        }
    }
}
