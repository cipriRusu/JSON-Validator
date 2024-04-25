using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Xunit.Sdk;

namespace JSONValidatorTest
{
    public class ValidateJsonInput
    {
        static readonly Range digit = new Range('0', '9');
        static readonly Range digitNoZero = new Range('1', '9');

        private enum NumberState
        {
            Invalid,
            StartState,
            NaturalNumberState,
            LeadingZeroState,
            LeadingNegativeState,
            DecimalState,
            FractionalState,
            ExponentialState,
            PostExponentialState
        }

        public static bool JsonStringValidator(string inputJsonString)
        {
            if (string.IsNullOrEmpty(inputJsonString))
            {
                return false;
            }

            if (!IsStringDelimitedByQuotes(inputJsonString))
            {
                return false;
            }

            if (IsStringContainingExtraQuotations(inputJsonString))
            {
                return false;
            }

            if (!IsEscapeValueValid(inputJsonString))
            {
                return false;
            }

            if (!IsUnicodeValueValid(inputJsonString))
            {
                return false;
            }

            return true;
        }

        public static bool JsonNumberValidator(string inputJsonNumber)
        {
            if (string.IsNullOrEmpty(inputJsonNumber)) return false;

            var currentState = NumberState.StartState;

            foreach (var current in inputJsonNumber)
            {
                switch (currentState)
                {
                    case NumberState.StartState:
                        currentState = HandleStartState(current);
                        break;
                    case NumberState.NaturalNumberState:
                        currentState = HandleNaturalNumberState(current);
                        break;
                    case NumberState.LeadingZeroState:
                        currentState = HandleLeadingZeroState(current);
                        break;
                    case NumberState.LeadingNegativeState:
                        currentState = HandleLeadingNegativeState(current);
                        break;
                    case NumberState.DecimalState:
                        currentState = HandleDecimalState(current);
                        break;
                    case NumberState.FractionalState:
                        currentState = HandleFractionalState(current);
                        break;
                    case NumberState.ExponentialState:
                        currentState = HandleExponentialState(current);
                        break;
                    case NumberState.PostExponentialState:
                        currentState = HandlePostExponentialState(current);
                        break;
                }
            }

            if (currentState == NumberState.DecimalState) return false;

            if (currentState == NumberState.ExponentialState) return false;

            return currentState != NumberState.Invalid;
        }

        private static bool IsUnicodeValueValid(string input)
        {
            const int UNICODEVALUELENGTH = 6;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '\\' && i + 1 == input.Length - 1)
                {
                    return false;
                }

                if (input[i] == '\\' && input[i + 1] == 'u')
                {
                    return input.Length - i >= UNICODEVALUELENGTH
                           && IsHexaValue(input.Substring(i + 2, 4));
                }
            }

            return true;
        }

        private static bool IsEscapeValueValid(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '\\' && i + 1 == input.Length - 1)
                {
                    return false;
                }

                if (input[i] == '\\' && (i + 1 == input.Length || !IsEscapeCharValid(input[i + 1])))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsEscapeCharValid(char input)
        {
            return "\"\\/bfnrtu".Contains(input);
        }

        private static bool IsStringDelimitedByQuotes(string input)
        {
            return input.Length >= 2
                   && input[0].Equals('"')
                   && input[input.Length - 1].Equals('"');
        }

        private static bool IsStringContainingExtraQuotations(string input)
        {
            for (int i = 1; i < input.Length - 2; i++)
            {
                if (input[i] == '"')
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsHexaValue(string input)
        {
            foreach (var c in input)
            {
                if (!IsInRange(c, new[]
                    {
                        new Range('0', '9'),
                        new Range('a', 'f'),
                        new Range('A', 'F')
                    })
                )

                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsInRange(char c, Range[] ranges)
        {
            foreach (var r in ranges)
            {
                if (r.Contains(c))
                {
                    return true;
                }
            }

            return false;
        }

        private struct Range
        {
            private readonly char start;
            private readonly char end;

            public Range(char start, char end)
            {
                this.start = start;
                this.end = end;
            }

            public bool Contains(char c)
            {
                return start <= c && c <= end;
            }
        }

        private static NumberState HandleStartState(char current)
        {
            if (current.Equals('-')) return NumberState.LeadingNegativeState;

            if (current.Equals('0')) return NumberState.LeadingZeroState;

            if (digit.Contains(current)) return NumberState.NaturalNumberState;

            return NumberState.Invalid;
        }

        private static NumberState HandleNaturalNumberState(char current)
        {
            if (digit.Contains(current)) return NumberState.NaturalNumberState;

            if (current.Equals('.')) return NumberState.DecimalState;

            if (current.Equals('E') || current.Equals('e')) return NumberState.ExponentialState;

            return NumberState.Invalid;
        }

        private static NumberState HandleLeadingZeroState(char current)
        {
            if (current == '.') return NumberState.DecimalState;

            if (current == 'E' || current == 'e') return NumberState.ExponentialState;

            return NumberState.Invalid;
        }

        private static NumberState HandleLeadingNegativeState(char current)
        {
            if (digitNoZero.Contains(current)) return NumberState.NaturalNumberState;

            if (current == '0') return NumberState.LeadingZeroState;

            return NumberState.Invalid;
        }

        private static NumberState HandleDecimalState(char current)
        {
            if (digit.Contains(current)) return NumberState.FractionalState;

            return NumberState.Invalid;
        }

        private static NumberState HandleFractionalState(char current)
        {
            if (digit.Contains(current)) return NumberState.FractionalState;

            if (current == 'E' || current == 'e') return NumberState.ExponentialState;

                return NumberState.Invalid;
        }

        private static NumberState HandleExponentialState(char current)
        {
            if (current == '+' || current == '-') return NumberState.ExponentialState;

            if (digit.Contains(current)) return NumberState.PostExponentialState;

            return NumberState.Invalid;
        }

        private static NumberState HandlePostExponentialState(char current)
        {
            if (digit.Contains(current)) return NumberState.PostExponentialState;

            return NumberState.Invalid;
        }
    }
}