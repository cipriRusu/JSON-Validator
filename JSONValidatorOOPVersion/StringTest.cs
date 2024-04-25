using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JSONValidatorAlternativeVersion
{
    public class StringTest
    {
        [Fact]
        public void StringReturnsTrueForValidEmptyStringValue()
        {
            var testString = new String();
            Assert.True(testString.Match("\"\"").Success());
        }

        [Fact]
        public void StringReturnsEmptyOutputForValidStringWithEscapeSequence()
        {
            var testString = new String();
            Assert.Equal("", testString.Match("\"asd\\tasd\"").RemainingText());
        }

        [Fact]
        public void StringReturnsProperOutputForExtraQuotes()
        {
            var testString = new String();
            Assert.Equal("\"", testString.Match("\"\"\"").RemainingText());
        }

        [Fact]
        public void StringReturnsTrueForValidStringNoEscapeSequence()
        {
            var testString = new String();
            Assert.True(testString.Match("\"something\"").Success());
        }

        [Fact]
        public void StringReturnsEmptyForValidStringNoEscapeSequence()
        {
            var testString = new String();
            Assert.Equal("", testString.Match("\"something\"").RemainingText());
        }

        [Fact]
        public void StringReturnsFalseForNoLeadingQuotes()
        {
            var testString = new String();
            Assert.False(testString.Match("something\"").Success());
        }

        [Fact]
        public void StringReturnsValidOutputForNoLeadingQuotes()
        {
            var testString = new String();
            Assert.Equal("something\"", testString.Match("something\"").RemainingText());
        }

        [Fact]
        public void StringReturnsFalseForNoTrailingQuotes()
        {
            var testString = new String();
            Assert.False(testString.Match("\"something").Success());
        }

        [Fact]
        public void StringReturnsValidOutputForNoTrailingQuotes()
        {
            var testString = new String();
            Assert.Equal("\"something", testString.Match("\"something").RemainingText());
        }

        [Fact]
        public void StringReturnsValidForHexadecimalsSequence()
        {
            var testString = new String();
            Assert.Equal("", testString.Match("\"\u1234abc\"").RemainingText());
        }

        [Fact]
        public void StringReturnsValidOutputForIncompleteHexadecimalCharacters()
        {
            var testString = new String();
            Assert.Equal("\"\\u123\"", testString.Match("\"\\u123\"").RemainingText());
        }
    }
}
