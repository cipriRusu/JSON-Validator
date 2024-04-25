using System;

namespace JSONValidatorAlternativeVersion
{
    internal class Many : IPattern
    {
        private readonly IPattern pattern;

        public Many(IPattern pattern)
        { this.pattern = pattern ?? null; }

        public IMatch Match(string text)
        {
            var match = pattern.Match(text);

            while (match.Success())
            {
                match = pattern.Match(match.RemainingText());
            }

            return new Match(match.RemainingText(), true);
        }
    }
}
