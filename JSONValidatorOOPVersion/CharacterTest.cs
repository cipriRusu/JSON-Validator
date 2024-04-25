using System;
using Xunit;

namespace JSONValidatorAlternativeVersion
{
    public class CharacterTest
    {
        [Fact]
        public void MatchReturnsTrueForValidFirstChar()
        {
            Character testCharObject = new Character('x');
            Assert.True(testCharObject.Match("xavier").Success());
        }

        [Fact]
        public void MatchReturnsFalseForInvalidFirstChar()
        {
            Character testCharObject = new Character('x');
            Assert.False(testCharObject.Match("Gicu").Success());
        }

        [Fact]
        public void MatchReturnsFalseForNullValue()
        {
            Character testCharObject = new Character('x');
            Assert.False(testCharObject.Match(null).Success());
        }

        [Fact]
        public void MatchReturnsFalseForEmptyValue()
        {
            Character testsCharObject = new Character('x');
            Assert.False(testsCharObject.Match("").Success());
        }


        [Fact]
        public void RemainingTextConsumesTheFirstCharacterWhenMatching()
        {
            Character testsCharObject = new Character('x');
            Assert.Equal("yz", testsCharObject.Match("xyz").RemainingText());
        }

        [Fact]
        public void RemainingTextReturnsFullStringWhenNotMatching()
        {
            Character testCharObject = new Character('x');
            Assert.Equal("abc", testCharObject.Match("abc").RemainingText());
        }
    }
}
