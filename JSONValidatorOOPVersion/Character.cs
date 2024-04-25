using System;

namespace JSONValidatorAlternativeVersion
{
    internal class Character : IPattern
    {
        private readonly char charPattern;

        public Character(char inputCharValue)
        { this.charPattern = inputCharValue; }

        public IMatch Match(string text)
        {
            return !string.IsNullOrEmpty(text)
                   && text[0] == charPattern        
                ? new Match(text.Substring(1), true)
                : new Match(text, false);
        }
    }
}
