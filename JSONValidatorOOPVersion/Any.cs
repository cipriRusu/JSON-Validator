using System;

namespace JSONValidatorAlternativeVersion
{
    class Any : IPattern
    {
        private readonly string accepted;

        public Any(string accepted)
        {
            this.accepted = accepted ?? string.Empty;
        }

        public IMatch Match(string text)
        {
            return !string.IsNullOrEmpty(text) && accepted.Contains(text[0])
                ? new Match(text.Substring(1), true)
                : new Match(text, false);
        }
    }
}

