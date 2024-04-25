using System;
using Xunit;

namespace JSONValidatorAlternativeVersion
{
    public class TextTest
    {
        [Fact]
        public void MatchReturnsFalseForNullValueAsText()
        {
            var testTextObject = new Text(null);
            Assert.False(testTextObject.Match("test").Success());
        }

        [Fact]
        public void MatchReturnsFalseForNullValueTrueInput()
        {
            var testTextObject = new Text("True");
            Assert.False(testTextObject.Match(null).Success());
        }

        [Fact]
        public void MatchReturnsFalseForNullValueFalseInput()
        {
            var testTextObject = new Text("False");
            Assert.False(testTextObject.Match(null).Success());
        }

        [Fact]

        public void MatchReturnFalseForNullValueEmptyInput()
        {
            var testTextObject = new Text("");
            Assert.False(testTextObject.Match(null).Success());
        }

        [Fact]
        public void MatchReturnsTrueForEmptyStringValue()
        {
            var testTextObject = new Text("");
            Assert.True(testTextObject.Match("true").Success());
        }

        [Fact]
        public void MatchReturnsValidStringForEmptyStringValue()
        {
            var testTextObject = new Text("");
            Assert.Equal("something", 
                testTextObject.Match("something").RemainingText());
        }
        
        [Fact]
        public void MatchReturnsTrueForValidPrefixStringValue()
        {
            var testTextObject = new Text("true");
            Assert.True(testTextObject.Match("truex").Success());
        }

        [Fact]
        public void MatchReturnsValidOutputForValidPrefixStringValue()
        {
            var testTextObject = new Text("true");
            Assert.Equal("x", testTextObject.Match("truex").RemainingText());
        }

        [Fact]
        public void MatchReturnsFalseForInvalidPrefixStringValue()
        {
            var testTextObject = new Text("False");
            Assert.False(testTextObject.Match("true").Success());
        }

        [Fact]
        public void MatchReturnsValidStringForInvalidPrefixStringValue()
        {
            var testTextObject = new Text("False");
            Assert.Equal("true", testTextObject.Match("true").RemainingText());
        }

        [Fact]
        public void MatchReturnsFalseForEmptyStringValue()
        {
            var testTextObject = new Text("True");
            Assert.False(testTextObject.Match("").Success());
        }
    }
}
