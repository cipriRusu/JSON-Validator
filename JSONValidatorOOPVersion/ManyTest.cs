using System;
using Xunit;

namespace JSONValidatorAlternativeVersion
{
    public class ManyTest
    {
        [Fact]
        public static void MatchReturnsTrueForPresentPatternAndNullInputValue()
        {
            var manyTestObject = new Many(new Character('a'));
            Assert.True(manyTestObject.Match(null).Success());
        }
        
        [Fact]
        public static void MatchreturnsTrueForPresentPatternAndValidInputValue()
        {
            var manyTestObject = new Many(new Character('a'));
            Assert.True(manyTestObject.Match("abc").Success());
        }

        [Fact]
        public static void MatchReturnsValidOutputForPresentPatternAndNullInputValue()
        {
            var manyTestObject = new Many(new Character('a'));
            Assert.Null(manyTestObject.Match(null).RemainingText());
        }

        [Fact]
        public void MatchReturnsValidOutputForPresentPatternAndEmptyStringInputValue()
        {
            var manyTestObject = new Many(new Character('a'));
            Assert.Equal("", manyTestObject.Match("").RemainingText());
        }

        [Fact(DisplayName = "True output for valid not present")]
        public void MatchReturnsTrueValueForPresentPatternAndStringNotContainingPattern()
        {
            var manyTestObject = new Many(new Character('a'));
            Assert.True(manyTestObject.Match("bc").Success());

        }

        [Fact(DisplayName = "Returns full string value if pattern not contained")]
        public void ReturnStringReturnsFullStringValueIfPatternNotContained()
        {
            var manyTestObject = new Many(new Character('a'));
            Assert.Equal("bc", manyTestObject.Match("bc").RemainingText());
        }

        [Fact(DisplayName = "Returns true if value is contained in pattern")]
        public void SucessReturnsTrueIfValueIsContainedInPattern()
        {
            var manyTestOBject = new Many(new Character('a'));
            Assert.True(manyTestOBject.Match("abc").Success());
        }

        [Fact(DisplayName = "Returns correct string value if first char is contained in pattern")]
        public void ReturnsCorrectStringValueIfPatternsContainFirstCharacter()
        {
            var manyTestObject = new Many(new Character('a'));
            Assert.Equal("bc", manyTestObject.Match("abc").RemainingText());
        }

        [Fact(DisplayName = "Returns true for multiple range characters leading string")]
        public void SuccessReturnsTrueForMultipleRangeCharactersLeadingString()
        {
            var manyTestObject = new Many(new Range('0', '9'));
            Assert.True(manyTestObject.Match("12345aab123").Success());
        }

        [Fact(DisplayName = "Returns proper string value for invalid string and rage of characters")]
        public void RemainingStringReturnsProperStringValueForInvalidLeadingRange()
        {
            var manyTestObject = new Many(new Range('0', '5'));
            Assert.Equal("abcd", manyTestObject.Match("abcd").RemainingText());
        }

        [Fact(DisplayName = "Returns proper string value for valid string and range of characters")]
        public void ReturnStringReturnsProperStringValueForValidStringAndRangeOfCharacters()
        {
            var manyTestObject = new Many(new Range('0', '5'));
            Assert.Equal("abc123", manyTestObject.Match("12345abc123").RemainingText());
        }

        [Fact(DisplayName = "Returns proper string value for valid string and sequence")]
        public void RetturnStringReturnsProperStringValueForValidStringAndSequence()
        {
            var manyTestObject = new Many(
                new Sequence(new Character('a'), 
                new Character('b'), 
                new Character('c')));

            Assert.Equal("123", manyTestObject.Match("abc123").RemainingText());
        }
    }
}
