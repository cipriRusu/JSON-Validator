using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JSONValidatorAlternativeVersion
{
    public class OneOrMoreTest
    {
        [Fact(DisplayName = "Match returns false for null as text value input")]
        public void MatchReturnsFalseForNullAsTextValueAndValidPattern()
        {
            var oneOrMoreTestObject = new OneOrMore(new Range('0', '5'));
            Assert.False(oneOrMoreTestObject.Match(null).Success());
        }
        [Fact(DisplayName = "Return text returns proper string value for null text and valid pattern")]
        public void ReturnTextReturnsProperStringValueForNullTextAndValidPattern()
        {
            var oneOrMoreTestObject = new OneOrMore(new Range('0', '5'));
            Assert.Null(oneOrMoreTestObject.Match(null).RemainingText());
        }

        [Fact(DisplayName = "Match returns false for pattern not found in input text")]
        public void MatchReturnsFalseForPatternNotFoundInInputText()
        {
            var oneOrMoreTestObject = new OneOrMore(new Range('0', '5'));
            Assert.False(oneOrMoreTestObject.Match("text").Success());
        }

        [Fact(DisplayName = "Remaining text returns proper string after pattern not found")]
        public void RemainingTextReturnsProperStringAfterPatternNotFound()
        {
            var oneOrMoreTestObject = new OneOrMore(new Range('0', '5'));
            Assert.Equal("text", oneOrMoreTestObject.Match("text").RemainingText());
        }

        [Fact(DisplayName = "Match returns true for pattern present in input text")]
        public void MatchReturnsTrueForPatternPresentInInputText()
        {
            var oneOrMoreTestObject = new OneOrMore(new Range('1', '9'));
            Assert.True(oneOrMoreTestObject.Match("1a").Success());
        }

        [Fact(DisplayName = "Remaining text returns proper string value for pattern present in input")]
        public void RemainingTextReturnsProperStringValue()
        {
            var oneOrMoreTestObject = new OneOrMore(new Range('1', '9'));
            Assert.Equal("a", oneOrMoreTestObject.Match("1a").RemainingText());
        }

        [Fact(DisplayName = "Match returns true for longer pattern present in input text")]
        public void MatchReturnsTrueForLongerPatternPresentInInputText()
        {
            var oneOrMoreTestObject = new OneOrMore(new Range('1', '9'));
            Assert.True(oneOrMoreTestObject.Match("123a").Success());
        }

        [Fact(DisplayName = "Remaining text returns proper string value for longer pattern present in input")]
        public void RemainingTextReturnsProperStringValueForLongerPattern()
        {
            var oneOrMoreTestObject = new OneOrMore(new Range('1', '9'));
            Assert.Equal("a", oneOrMoreTestObject.Match("123a").RemainingText());
        }
    }
}
