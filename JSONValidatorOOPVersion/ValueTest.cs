using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JSONValidatorAlternativeVersion
{
    public class ValueTest
    {
        [Fact]
        public void MatchReturnsTrueForValidInputValue()
        {
            var value = new Value();
            Assert.True(value.Match("\"\"").Success());
        }

        [Fact]
        public void ReturnTextReturnsEmptyStringForValidInputValue()
        {
            var value = new Value();
            Assert.Equal("", value.Match(
                "121.3E+3").RemainingText());
        }

        [Fact]
        public void ReturnTextReturnsRemainingValueForValidInputValue()
        {
            var value = new Value();
            Assert.Equal("1234.324E+5",
                value.Match("\"Something\" 1234.324E+5").RemainingText());
        }

        [Fact]
        public void MatchReturnsTrueForValidInputValueAndLargeSpace()
        {
            var value = new Value();
            Assert.True(value.Match("    \"\"  ").Success());
        }

        [Fact]
        public void MatchReturnsFalseForInvalidInputValue()
        {
            var value = new Value();
            Assert.False(value.Match("\"").Success());
        }

        [Fact]
        public void MatchReturnsTrueForValidComplexValue()
        {
            var value = new Value();
            Assert.True(
                value.Match(
                    "       \"TestTextInput \\t" +
                    "\\u1234 TestTeST" +
                    "\"                 "
                    ).Success());
        }

        [Fact]
        public void MatchReturnsTrueForValidComplexValueAndLargeSpaces()
        {
            var value = new Value();
            Assert.True(
                value.Match(
                    "\"             TestInputValue" +
                    "\\u121A Test\"                 "
                    ).Success());
        }

        [Fact]
        public void MatchReturnsFalseForInvalidComplexValueAndLargeSpaces()
        {
            var value = new Value();

            Assert.False(
                value.Match(
                    "\"             TestInputValue" +
                    "\\u121X Test\"                 "
                    ).Success());
        }

        [Fact]
        public void MatchReturnsTrueForValidComplexValueLargeSpacesAllDataTypes()
        {
            var value = new Value();

            Assert.True(
                value.Match(
                    "\"" +
                    "Some random text value" +
                    "\"" +
                    "false" +
                    "123.34E+3" +
                    "true" +
                    "null"
                    ).Success());
        }

        [Fact]
        public void MatchReturnsTrueForArrayInsertedIntoValue()
        {
            var value = new Value();
            Assert.True(value.Match("[1, 2, 3]").Success());
        }

        [Fact]
        public void MatchReturnsFalseForInvlidIntegerArray()
        {
            var array = new Value();
            Assert.False(array.Match("[1, 2,]").Success());
        }

        [Fact]
        public void MatchReturnsTrueForValidStringArray()
        {
            var array = new Value();
            Assert.True(
                array.Match("[\"first\", \"second\", \"third\"]").
                Success());
        }

        [Fact]
        public void MatchReturnsTrueForValidMultipleTypesArray()
        {
            var array = new Value();
            Assert.True(
                array.Match("[\"first\", 1234, \"second\"]").
                Success());
        }

        [Fact]
        public void ReturnTextReturnsEmptyStringForCorrectArray()
        {
            var array = new Value();
            Assert.Equal("", array.Match("[121.345, \"something\"]").
                RemainingText());
        }

        [Fact]
        public void ReturnTextReturnsFullStringForIncorrectArray()
        {
            var array = new Value();
            Assert.Equal("[121.4566 \"somethingElse\"]",
                array.Match("[121.4566 \"somethingElse\"]").
                RemainingText());
        }

        [Fact]
        public void MatchReturnsTrueForEmptyObject()
        {
            var obj = new Value();
            Assert.True(obj.Match("{}").Success());
        }

        [Fact]
        public void MatchReturnsTrueForValidSingleElement()
        {
            var obj = new Value();
            Assert.True(obj.Match(
                "{ \"name\":\"John\" }").
                Success());
        }

        [Fact]
        public void MatchReturnsTrueForValidObjectMultipleElements()
        {
            var obj = new Value();
            Assert.True(obj.Match(
                "{ \"name\":\"John\", \"age\":30 }").
                Success());
        }

        [Fact]
        public void MatchReturnsFalseForInvalidObjectMissingComma()
        {
            var obj = new Value();
            Assert.False(obj.Match(
                "{ \"name\":\"John\", \"age\":30 \"car\":null }").
                Success());
        }

        [Fact]
        public void MatchReturnsTrueForValidObjectContainingAllTypesOfValues()
        {
            var obj = new Value();
            Assert.True(obj.Match("{ \"name\": \"John\", \"age\": 30, \"hasCar\": true, \"isIll\": false, \"needsMoney\": null}").Success()); ;
        }

        [Fact]
        public void MatchReturnsFalseForInvalidObjectContainingInvalidValue()
        {
            var obj = new Value();
            Assert.False(obj.Match("{ \"name\": \"John\", \"age\": 30, \"hasCar\": true, \"isIll\": fals, \"needsMoney\": null}").Success());
        }
    }
}