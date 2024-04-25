using Xunit;

namespace JSONValidatorAlternativeVersion
{
    public class MatchTest
    {
        [Fact]
        public void StringInputReturnsProperOutputTrueValue()
        {
            var inputMatch = new Match("abcd", true);
            Assert.True(inputMatch.Success());
        }

        [Fact]
        public void StringInputReturnsProperOutputFalseValue()
        {
            var inputMatch = new Match("efgh", false);
            Assert.False(inputMatch.Success());
        }

        [Fact]
        public void StringInputReturnsProperOutputNullValue()
        {
            var inputMatch = new Match(null, true);
            Assert.True(inputMatch.Success());
        }

        [Fact]
        public void StringInputReturnsProperOutputEmptyValue()
        {
            var inputMatch = new Match("", false);
            Assert.False(inputMatch.Success());
        }

        [Fact]
        public void StringInputReturnsProperOutputFullString()
        {
            var inputMatch = new Match("abcd", true);
            Assert.Equal("abcd", inputMatch.RemainingText());
        }
    }
}