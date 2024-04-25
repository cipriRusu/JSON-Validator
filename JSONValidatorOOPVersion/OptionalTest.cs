using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JSONValidatorAlternativeVersion
{
    public class OptionalTest
    {
        [Fact(DisplayName = "Match returns true for empty string value as text")]
        public void MatchReturnsTrueForEmptyString()
        {
            var optionalTestObject = new Optional(new Character('a'));

            Assert.True(optionalTestObject.Match("").Success());
        }

        [Fact(DisplayName = "RemainingText returns proper value for empty string as text")]
        public void RemainingTextReturnsProperValueForEmptyStringAsText()
        {
            var optionalTestObject = new Optional(new Character('a'));

            Assert.Equal("", optionalTestObject.Match("").RemainingText());
        }

        [Fact(DisplayName = "Match return true for null string value as text")]
        public void MatchReturnsTrueForNullStringValueAsText()
        {
            var optionalTestObject = new Optional(new Character('a'));

            Assert.True(optionalTestObject.Match(null).Success());
        }

        [Fact(DisplayName = "RemainingText returns proper value for null value as string text")]
        public void RemainingTextReturnsProperValueAsNullValueStringText()
        {
            var optionalTestObject = new Optional(new Character('a'));

            Assert.Null(optionalTestObject.Match(null).RemainingText());
        }

        [Fact(DisplayName = "Match returns true for one character from string")]
        public void MatchReturnsTrueForOneSingleCharacterValueAndStringInput()
        {
            var optionalTestObject = new Optional(new Character('a'));

            Assert.True(optionalTestObject.Match("abc").Success());
        }

        [Fact(DisplayName = "Remaining text returns proper string output from initial string")]
        public void RemainingTextReturnsProperStringValueFromInitialString()
        {
            var optionalTestObject = new Optional(new Character('a'));

            Assert.Equal("bc", optionalTestObject.Match("abc").RemainingText());
        }

        [Fact(DisplayName = "Match returns true for one character from invalid string")]
        public void MatchReturnsTrueWithInitialInvalidString()
        {
            var optionalTestProject = new Optional(new Character('a'));

            Assert.True(optionalTestProject.Match("xbc").Success());
        }

        [Fact(DisplayName = "Remaining text returns proper string from output from initial string invalid value")]
        public void RemainingTextReturnsProperValueFromInitialInvalidString()
        {
            var optionalTestProject = new Optional(new Character('a'));

            Assert.Equal("xbc", optionalTestProject.Match("xbc").RemainingText());
        }

        [Fact(DisplayName = "Match returns true for range value from string")]
        public void MatchReturnsTrueForRangeValueFromInitialString()
        {
            var optionalTestProject = new Optional(new Range('0', '5'));

            Assert.True(optionalTestProject.Match("0013").Success());
        }

        [Fact(DisplayName = "Remaining text returns proper string output from valid string")]
        public void RemainingTextReturnsProperOutputValueFromInitialValidString()
        {
            var optinalTestProject = new Optional(new Range('0', '5'));

            Assert.Equal("013", optinalTestProject.Match("0013").RemainingText());
        }

        [Fact(DisplayName = "Match returns true for pattern not present in input string")]
        public void MatchReturnsTrueForPatternNotPresentInInputString()
        {
            var optionalTestProject = new Optional(new Range('0', '5'));

            Assert.True(optionalTestProject.Match("bc").Success());
        }

        [Fact(DisplayName = "RemainingText returns proper string output from invalid string")]
        public void RemainingTextReturnsProperStringOutputFromPatternNotPresent()
        {
            var optionalTestProject = new Optional(new Range('0', '5'));

            Assert.Equal("bc", optionalTestProject.Match("bc").RemainingText());
        }

        [Fact(DisplayName = "Match returns true for sequence pattern in input string")]
        public void MatchReturnsTrueForSequenceInInputString()
        {
            var optionalTestProject = new Optional(
                new Sequence(new Character('a'),
                new Range('0', '4')));

            Assert.True(optionalTestProject.Match("a4xyz").Success());
        }

        [Fact(DisplayName = "RemainingText returns proper string output from valid string")]
        public void RemainingTextReturnsProperStringOutputFromValidString()
        {
            var optionalTestProject = new Optional(
            new Sequence(new Character('a'),
            new Range('0', '4')));

            Assert.Equal("xyz", optionalTestProject.Match("a4xyz").RemainingText());
        }
        [Fact(DisplayName = "Match returns true for sequence pattern not in input string")]
        public void MatchReturnsTrueForSequencePatternNotInInputString()
        {
            var optionalTestProject = new Optional(
                            new Sequence(new Character('a'),
                            new Range('0', '4')));

            Assert.True(optionalTestProject.Match("xyyz").Success());

        }

        [Fact(DisplayName = "RemainingText returns proper string output from invalid string")]
        public void RemainingTextReturnsProperStringOutputFromInValidString()
        {
            var optionalTestProject = new Optional(
            new Sequence(new Character('a'),
            new Range('0', '4')));

            Assert.Equal("xxyz", optionalTestProject.Match("xxyz").RemainingText());
        }

        [Fact(DisplayName = "RemainingText returns proper string output from valid string choice pattern")]
        public void RemainingTextReturnsProperStringOutputFromValidStringChoicePattern()
        {
            var optionalTestProject = new Optional(
            new Choice(new Character('a'),
            new Range('0', '4')));

            Assert.Equal("axyz", optionalTestProject.Match("4axyz").RemainingText());
        }
    }
}

