using Xunit;

namespace JSONValidatorAlternativeVersion
{
    public class RangeTest
    {
        [Fact]
        public void RangeReturnsTrueForValidInterval()
        {
            var testRangeObject = new Range('a', 'f');
            Assert.True(testRangeObject.Match("abc").Success());
        }

        [Fact]
        public void RangeReturnsFalseForValidIntervalInvalidString()
        {
            var testRangeObject = new Range('a', 'f');
            Assert.False(testRangeObject.Match("1ab").Success());
        }

        [Fact]
        public void RangeReturnsTrueForValidIntervalUnSorted()
        {
            var testRangeObject = new Range('a', 'f');
            Assert.True(testRangeObject.Match("fab").Success());
        }

        [Fact]
        public void RangeReturnsFalseForNullInput()
        {
            var testRangeObject = new Range('a', 'f');
            Assert.False(testRangeObject.Match(null).Success());
        }

        [Fact]
        public void RangeReturnsFalseForEmptyInput()
        {
            var testRangeObject = new Range('a', 'f');
            Assert.False(testRangeObject.Match("").Success());
        }

        [Fact]
        public void RangeReturnsValidTextForMatchingChar()
        {
            var testRangeObject = new Range('a', 'i');
            Assert.Equal("fi", testRangeObject.Match("afi").RemainingText());
        }

        [Fact]
        public void RangeReturnsValidTextForUnMatchingChar()
        {
            var testRangeObject = new Range('a', 'i');
            Assert.Equal("jfi", testRangeObject.Match("jfi").RemainingText());
        }

        [Fact]
        public void RangeReturnsTrueForValidExcludedCharacter()
        {
            var testRangeObject = new Range('a', 'e', "u");
            Assert.True(testRangeObject.Match("abcde").Success());
        }

        [Fact]
        public void RangeReturnsFullStringOutputForExcludedCharacter()
        {
            var testRangeObject = new Range('a', 'e', "cb");
            Assert.Equal("c1234", testRangeObject.Match("c1234").RemainingText());
        }

        [Fact]
        public void RangeReturnsCorrectOutputForExcludedCharacter()
        {
            var testRangeObject = new Range('a', 'e', "cb");
            Assert.Equal("1234", testRangeObject.Match("a1234").RemainingText());
        }

        [Fact]
        public void RangeReturnsFalseForValidExcludedCharacterIncludedInString()
        {
            var testRangeObject = new Range('a', 'e', "u");
            Assert.False(testRangeObject.Match("u1234").Success());
        }
    }
}