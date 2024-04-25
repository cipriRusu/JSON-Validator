using System;

namespace JSONValidatorAlternativeVersion
{
    internal class Range : IPattern
    {
        private char startChar;
        private char endChar;
        private string excluded;

        public Range(char inputStartChar, char inputEndChar, string excluded = "")
        {
            this.startChar = inputStartChar;
            this.endChar = inputEndChar;
            this.excluded = excluded;
        }

        public IMatch Match(string text)
        {
            return !string.IsNullOrEmpty(text) &&
                startChar <= text[0] &&
                text[0] <= endChar &&
                !excluded.Contains(text[0])
            ? new Match(text.Substring(1), true)
            : new Match(text, false);
        }
    }
}
