using System;
using Xunit;

namespace JSONValidatorTest
{
    public class ValidateJsonInputTest
    {
        [Fact]
        public void JsonStringValidatorReturnsFalseForInputNullValue()
        {
            string inputJsonString = null;
            Assert.False(ValidateJsonInput.JsonStringValidator(inputJsonString));
        }

        [Fact]
        public void JsonStringValidatorReturnsFalseForEmptyUnicodeString()
        {
            var inputJsonString = "\"TestValue\\u\"";
            Assert.False(ValidateJsonInput.JsonStringValidator(inputJsonString));
        }

        [Fact]
        public void JsonStringValidatorReturnsFalseForSingleQuotationString()
        {
            var inputJsonString = "\"";
            Assert.False(ValidateJsonInput.JsonStringValidator(inputJsonString));
        }

        [Fact]
        public void JsonStringTestReturnsFalseForUnderLengthUnicodeString()
        {
            var inputJsonString = "\"\\\"";
            Assert.False(ValidateJsonInput.JsonStringValidator(inputJsonString));
        }

        [Fact]
        public void JsonStringValidatorReturnsFalseForAbsentQuotations()
        {
            var inputJsonString = "InputString";
            Assert.False(ValidateJsonInput.JsonStringValidator(inputJsonString));
        }

        [Fact]
        public void JsonStringValidatorReturnsFalseForAbsentQLeft()
        {
            var inputJsonString = "\"InputString";
            Assert.False(ValidateJsonInput.JsonStringValidator(inputJsonString));
        }

        [Fact]
        public void JsonStringValidatorReturnsTrueForEmptyString()
        {
            var inputJsonString = "\"\"";
            Assert.True(ValidateJsonInput.JsonStringValidator(inputJsonString));
        }

        [Fact]
        public void JsonStringValidatorReturnsFalseForAbsentQRight()
        {
            var inputJsonString = "InputString\"";
            Assert.False(ValidateJsonInput.JsonStringValidator(inputJsonString));
        }

        [Fact]
        public void JsonStringValidatorReturnsTrueForPresentQuotations()
        {
            var inputJsonString = "\"InputString\"";
            Assert.True(ValidateJsonInput.JsonStringValidator(inputJsonString));
        }

        [Fact]
        public void JsonStringValidatorReturnsFalseForExtraQuotations()
        {
            var inputJsonString = "\"Input\"String\"";
            Assert.False(ValidateJsonInput.JsonStringValidator(inputJsonString));
        }

        [Fact]
        public void JsonStringValidatorReturnsFalseForInvalidEscapeSequence()
        {
            var inputJsonString = "\"InputStrin\\g\"";
            Assert.False(ValidateJsonInput.JsonStringValidator(inputJsonString));
        }

        [Fact]
        public void JsonStringValidatorReturnsFalseForMultipleEscapeSequencesOneValidOneInvalid()
        {
            var inputJsonString = "\"I\\nput\\String\"";
            Assert.False(ValidateJsonInput.JsonStringValidator(inputJsonString));
        }

        [Fact]
        public void JsonStringValidatorReturnsTrueForValidUnicodeValue()
        {
            var inputJsonString = "\"\\u009A\\u009a\"";
            Assert.True(ValidateJsonInput.JsonStringValidator(inputJsonString));
        }

        [Fact]
        public void JsonStringValidatorReturnsFalseForInvalidUnicodeValue()
        {
            var inputJsonString = "\"\\u009X\\u009x\"";
            Assert.False(ValidateJsonInput.JsonStringValidator(inputJsonString));
        }

        [Fact]
        public static void JsonStringValidatorReturnsTrueForComplexValidValue()
        {
            var inputJsonString = "\"InputComplexValid\\nValue\\u00AB\"";
            Assert.True(ValidateJsonInput.JsonStringValidator(inputJsonString));
        }

        [Fact]
        public static void JsonStringValidatorReturnsFalseForComplexInvalidValue()
        {
            var inputJsonString = "\"InputComplexValid\\gValue\\u00AB\"";
            Assert.False(ValidateJsonInput.JsonStringValidator(inputJsonString));
        }

        [Fact]
        public void ValidateJsonNumberInputReturnsFalseForEmptyString()
        {
            string JsonNumberInput = "";
            Assert.False(ValidateJsonInput.JsonNumberValidator(JsonNumberInput));
        }

        [Fact]
        public void ValidateJsonNumberInputReturnsTrueForValidInteger()
        {
            string JsonNumberInput = "1200";
            Assert.True(ValidateJsonInput.JsonNumberValidator(JsonNumberInput));
        }

        [Fact]
        public void ValidateJsonNumberInputReturnsFalseForLeadingZeroInteger()
        {
            string JsonNumberInput = "01200";
            Assert.False(ValidateJsonInput.JsonNumberValidator(JsonNumberInput));
        }

        [Fact]
        public void ValidateJsonNumberInputReturnsFalseForLetterInNumber()
        {
            string JsonNumberInput = "100A23";
            Assert.False(ValidateJsonInput.JsonNumberValidator(JsonNumberInput));
        }

        [Fact]
        public void ValidateJsonNumberInputReturnsTrueForNegativeNumber()
        {
            string JsonNumberInput = "-1200";
            Assert.True(ValidateJsonInput.JsonNumberValidator(JsonNumberInput));
        }

        [Fact]
        public void ValidateJsonNumberInputReturnsTrueForValidNegativeDecimalNumber()
        {
            string JsonNumberInput = "-12.223";
            Assert.True(ValidateJsonInput.JsonNumberValidator(JsonNumberInput));
        }

        [Fact]
        public void ValidateJsonNumberInputReturnsFalseForNoValueAfterDecimalPoint()
        {
            string JsonNumberInput = "122.";
            Assert.False(ValidateJsonInput.JsonNumberValidator(JsonNumberInput));
        }

        [Fact]
        public void ValidateJsonNumberInputReturnsTrueForValidNegativeDecimalWithExponent()
        {
            string JsonNumberInput = "-1222.123313E+34";
            Assert.True(ValidateJsonInput.JsonNumberValidator(JsonNumberInput));
        }

        [Fact]
        public void ValidateJsonNumberInputReturnsFalseForInvalidNegativeValue()
        {
            string JsonNumberInput = "-0121";
            Assert.False(ValidateJsonInput.JsonNumberValidator(JsonNumberInput));
        }

        [Fact]
        public void ValidateJsonNumberInputReturnsTrueForValidExponentialInteger()
        {
            string JsonNumberInput = "111E+3";
            Assert.True(ValidateJsonInput.JsonNumberValidator(JsonNumberInput));
        }

        [Fact]
        public void ValidateJsonNumberInputReturnsFalseForExponentialNoExponent()
        {
            string JsonNumberInput = "121E+";
            Assert.False(ValidateJsonInput.JsonNumberValidator(JsonNumberInput));
        }

        [Fact]
        public void ValidateJsonNumberInputReturnsFalseForExponentAfterDecimal()
        {
            string JsonNumberInput = "122.E+2";
            Assert.False(ValidateJsonInput.JsonNumberValidator(JsonNumberInput));
        }

        [Fact]
        public void ValidateJsonNumberInputReturnsTrueForZeroAndExponent()
        {
            string JsonNumberInput = "0E+232145566";
            Assert.True(ValidateJsonInput.JsonNumberValidator(JsonNumberInput));
        }

        [Fact]
        public void ValidateJsonNumberInputReturnsFalseForTwoDecimalPoints()
        {
            string JsonNumberInput = "111.111E+2.11";
            Assert.False(ValidateJsonInput.JsonNumberValidator(JsonNumberInput));
        }

        [Fact]
        public void ValidateJsonNumberInputReturnsFalseForTwoExponents()
        {
            string JsonNumberInput = "11.1214E+23E344";
            Assert.False(ValidateJsonInput.JsonNumberValidator(JsonNumberInput));
        }

        [Fact]
        public void ValidateJsonNumberInputReturnsTrueForValidNegativeWithLeadingZero()
        {
            string JsonNumberInput = "-0.000121E+23";
            Assert.True(ValidateJsonInput.JsonNumberValidator(JsonNumberInput));
        }

        [Fact]
        public void ValidateJsonNumberReturnsFalseForNegativeDoubleLeadingZero()
        {
            string JsonNumberInput = "-00.011211+E+23";
            Assert.False(ValidateJsonInput.JsonNumberValidator(JsonNumberInput));
        }
    }
}