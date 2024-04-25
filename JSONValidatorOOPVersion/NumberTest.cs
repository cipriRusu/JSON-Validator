using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JSONValidatorAlternativeVersion
{
    public class NumberTest
    {
        [Fact]
        public void NumberReturnsTrueForValidInputMinusZero()
        {
            var number = new Number();
            Assert.Equal("", number.Match("-0").RemainingText());
        }

        [Fact]
        public void ReturnsValidStringForInvalidInputMinusLeadingZero()
        {
            var number = new Number();
            Assert.Equal("12", number.Match("-012").RemainingText());
        }

        [Fact]
        public void NumberReturnsTrueForValidJsonInputNumericValuesOnly()
        {
            var number = new Number();
            Assert.True(number.Match("1234").Success());
        }

        [Fact]
        public void ReturnTextReturnsEmptyStringForValidValue()
        {
            var number = new Number();
            Assert.Equal("", number.Match("1234").RemainingText());
        }

        [Fact]
        public void NumberReturnFalseForInvalidJsonInputNumericValuesOnly()
        {
            var number = new Number();
            Assert.True(number.Match("01234").Success());
        }

        [Fact]
        public void ReturnTextReturnsValidStringForInvalidInputNumericValuesOnly()
        {
            var number = new Number();
            Assert.Equal("1234", number.Match("01234").RemainingText());
        }

        [Fact]
        public void SuccesReturnsTrueForValidInputNegativeNumber()
        {
            var number = new Number();
            Assert.True(number.Match("-1234").Success());
        }

        [Fact]
        public void ReturnTextReturnsValidStringForValidNegativeInput()
        {
            var number = new Number();
            Assert.Equal("", number.Match("-1234").RemainingText());
        }

        [Fact]
        public void SuccesReturnsFalseForNegativeAndLeadingZeroValue()
        {
            var number = new Number();
            Assert.True(number.Match("-01234").Success());
        }

        [Fact]
        public void ReturnTextReturnsValidStringForInvalidNegativeInput()
        {
            var number = new Number();
            Assert.Equal("1234", number.Match("-01234").RemainingText());
        }

        [Fact]
        public void SuccesReturnsTrueForValidDecimalNumberValue()
        {
            var number = new Number();
            Assert.True(number.Match("12.34").Success());
        }

        [Fact]
        public void ReturnTextReturnsValidStringForValidDecimalNumberValue()
        {
            var number = new Number();
            Assert.Equal("", number.Match("12.34").RemainingText());
        }

        [Fact]
        public void SuccessReturnsFalseForInvalidNegativeNumberWithDecimal()
        {
            var number = new Number();
            Assert.True(number.Match("-0123.1").Success());
        }

        [Fact]
        public void ReturnTextReturnsValidStringForInValidDecimalNumberValue()
        {
            var number = new Number();
            Assert.Equal("123.4", number.Match("-0123.4").RemainingText());
        }

        [Fact]
        public void SuccessReturnsTrueForValidDecimalNegativeNumberWithLeadingZero()
        {
            var number = new Number();
            Assert.True(number.Match("-0.123").Success());
        }

        [Fact]
        public void ReturnTextReturnsValidStringForValidNegativeDecimalValue()
        {
            var number = new Number();
            Assert.Equal("", number.Match("-0.123").RemainingText());
        }

        [Fact]
        public void SuccessReturnsTrueForValidExponentialInFractionar()
        {
            var number = new Number();
            Assert.True(number.Match("1231.34E+2").Success());
        }

        [Fact]
        public void ReturnTextReturnsEmptyStringForValidExponentialInFractionar()
        {
            var number = new Number();
            Assert.Equal("", number.Match("1231.34E+2").RemainingText());
        }

        [Fact]
        public void SuccessReturnsTrueForExponentialOnlyValue()
        {
            var number = new Number();
            Assert.True(number.Match("0E+222431").Success());
        }

        [Fact]
        public void SuccessReturnsTrueForValidExponentialValueNoPlusMinus()
        {
            var number = new Number();
            Assert.True(number.Match("123.34E32").Success());
        }

        [Fact]
        public void RemainingTextReturnsValidOutputForWronglyPlacedExponentialValue()
        {
            var number = new Number();
            IMatch match = number.Match("1234.E+234");
            Assert.True(match.Success());
            Assert.Equal(".E+234", match.RemainingText());
        }
    }
}
