using Xunit;

namespace JSONValidatorAlternativeVersion
{
    public class SequenceTest
    {
        [Fact]
        public void MatchReturnsTrueForValidInput()
        {
            var demoSequence = new Sequence(
                new Character('a'),
                new Character('b'));

            Assert.True(demoSequence.Match("abcd").Success());
        }

        [Fact]
        public void MatchReturnsFalseForInvalidInput()
        {
            var demoSequence = new Sequence(
                new Character('a'),
                new Character('b')
            );

            Assert.False(demoSequence.Match("aef").Success()
            );
        }

        [Fact]
        public void MatchReturnsFalseForEmptyValue()
        {
            var demoSequence = new Sequence(
                new Character('a'),
                new Character('b')
            );

            Assert.False(demoSequence.Match("").Success());
        }

        [Fact]
        public void MatchReturnsFalseForNullValue()
        {
            var demoSequence = new Sequence(
                new Character('a'),
                new Character('b')
                );

            Assert.False(demoSequence.Match(null).
                Success());
        }

        [Fact]
        public void MatchReturnsTrueForNestedSequenceAndChar()
        {
            var demoSequence = new Sequence(
                new Character('a'),
                new Character('b'));

            var demoNestedSequence = new Sequence(
                demoSequence,
                new Character('c'));

            Assert.True(demoNestedSequence.Match("abcd").
                Success());
        }

        [Fact]
        public void MatchReturnsTrueForNestedTwoSequences()
        {
            var firstSequence =
                new Sequence(new Character('a'), new Character('b'));
            var secondSequence = new Sequence(new Character('x'), new Character('y'));

            var finalSequence = new Sequence(firstSequence, secondSequence);

            Assert.True(finalSequence.Match("abxy").Success());
        }

        [Fact]
        public void MatchReturnsTrueForHexadecimalRangeChoice()
        {
            var hexadecimalRangeChoice = new Choice(
                new Range('0', '9'),
                new Range('a', 'f'),
                new Range('A', 'F'));

            Assert.True(hexadecimalRangeChoice.Match("12AB").Success());
        }

        [Fact]
        public void SequenceReturnsTrueForValidCompleteHexadecimalValue()
        {
            var hexadecimalLeadingChar = new Character('u');

            var hexadecimalRangeChoice = new Choice(
                new Range('0', '9'),
                new Range('a', 'f'),
                new Range('A', 'F')
                );

            var hexadecimalFullSequence = new Sequence(
                hexadecimalLeadingChar,
                new Sequence
                (
                    hexadecimalRangeChoice,
                    hexadecimalRangeChoice,
                    hexadecimalRangeChoice,
                    hexadecimalRangeChoice));

            Assert.True(hexadecimalFullSequence.Match("uB00A").Success());
        }

        [Fact]
        public void SequenceReturnsFalseForInvalidCompleteHexadecimalValue()
        {
            var hexadecimalLeadingChar = new Character('u');

            var hexadecimalRangeChoice = new Choice(
                new Range('0', '9'),
                new Range('a', 'f'),
                new Range('A', 'F')
                );

            var hexadecimalFullSequence = new Sequence(
                hexadecimalLeadingChar,
                new Sequence
                (
                    hexadecimalRangeChoice,
                    hexadecimalRangeChoice,
                    hexadecimalRangeChoice,
                    hexadecimalRangeChoice));

            Assert.False(hexadecimalFullSequence.Match("uX1A3").Success());
        }

        [Fact]
        public void SequenceReturnsValidRemainingValueFromString()
        {
            var single = new Character('x');
            var range = new Range('0', '5');

            var sequence = new Sequence(
                single, new Sequence(range, range));

            Assert.Equal("a",
                sequence.Match("x15a").RemainingText());
        }

        [Fact]
        public void SequenceReturnsValidRemainingValueFromComplexString()
        {
            var singleLeadingChar = new Character('a');
            var range = new Range('0', '1');
            var secRange = new Range('a', 'h');

            var sequence = new Sequence(range, singleLeadingChar,
                new Sequence(secRange, secRange, range));

            Assert.Equal("i",
                sequence.Match("1aff0i").RemainingText());
        }

        [Fact]
        public void SequenceReturnsValidRemainingValueFromComplexStringIncludingWhiteSpace()
        {
            var hexadecimalLeadingChar = new Character('u');

            var hexadecimalRangeChoice = new Choice(
                new Range('0', '9'),
                new Range('a', 'f'),
                new Range('A', 'F')
                );

            var hexadecimalFullSequence = new Sequence(
                hexadecimalLeadingChar,
                new Sequence
                (
                    hexadecimalRangeChoice,
                    hexadecimalRangeChoice,
                    hexadecimalRangeChoice,
                    hexadecimalRangeChoice));

            Assert.Equal(" 45",
                hexadecimalFullSequence.Match("uab64 45").RemainingText());
        }

        [Fact]
        public void SequenceReturnsValidRemainingValueFromInvalidComplexString()
        {
            var hexadecimalLeadingChar = new Character('u');

            var hexadecimalRangeChoice = new Choice(
                new Range('0', '9'),
                new Range('a', 'f'),
                new Range('A', 'F')
                );

            var hexadecimalFullSequence = new Sequence(
                hexadecimalLeadingChar,
                new Sequence
                (
                    hexadecimalRangeChoice,
                    hexadecimalRangeChoice,
                    hexadecimalRangeChoice,
                    hexadecimalRangeChoice));

            Assert.Equal("uax64 45",
                hexadecimalFullSequence.Match("uax64 45").RemainingText());
        }
    }
}