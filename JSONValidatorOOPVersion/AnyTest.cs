using System;
using Xunit;

namespace JSONValidatorAlternativeVersion
{
    public class AnyTest
    {
        [Fact]
        public void MatchReturnsFalseForNullValue()
        {
            var testAnyObject = new Any("eE");
            Assert.False(testAnyObject.Match(null).Success());
        }

        [Fact]
        public void MatchReturnsFalseForNullAcceptedValue()
        {
            var testAnyObject = new Any(null);
            Assert.False(testAnyObject.Match("test").Success());
        }

        [Fact]
        public void MatchReturnsProperStringValueForNullAcceptedValue()
        {
            var testAnyObject = new Any(null);
            Assert.Equal("test", testAnyObject.Match("test").RemainingText());
        }

        [Fact]
        public void MatchReturnsFalseForEmptyStringValue()
        {
            var testAnyObject = new Any("eE");
            Assert.False(testAnyObject.Match("").Success());
        }

        [Fact]
        public void MatchReturnsNullStringForNullValue()
        {
            var testAnyObject = new Any("eE");
            Assert.Null(testAnyObject.Match(null).RemainingText());
        }

        [Fact]
        public void MatchReturnsEmptyStringForEmptyStringValue()
        {
            var testAnyObject = new Any("eE");
            Assert.Equal("", testAnyObject.Match("").RemainingText());
        }

        [Fact]
        public void MatchReturnsValidStringOutputForValidValue()
        {
            var testAnyObject = new Any("eE");
            Assert.Equal("a", testAnyObject.Match("ea").RemainingText());
        }

        [Fact]
        public void MatchReturnsValidStringOutputForValidValueSecondChar()
        {
            var testAnyObject = new Any("eE");
            Assert.Equal("a", testAnyObject.Match("Ea").RemainingText());
        }
        
        [Fact]
        public void MatchReturnsValidStringOutputForInvalidValue()
        {
            var testAnyObject = new Any("eE");
            Assert.Equal("xyz", testAnyObject.Match("xyz").RemainingText());
        }
    }
}
