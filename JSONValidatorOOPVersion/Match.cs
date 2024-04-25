using System;

namespace JSONValidatorAlternativeVersion
{
    internal class Match : IMatch
    {
        private readonly string currentString;
        private readonly bool isValid;

        public Match(string inputStringValue, bool isInputValid)
        {
            currentString = inputStringValue;
            isValid = isInputValid;
        }

        public bool Success()
        { return isValid; }

        public string RemainingText()
        { return currentString; }
    }
}
