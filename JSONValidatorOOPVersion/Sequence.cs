namespace JSONValidatorAlternativeVersion
{
    internal class Sequence : IPattern
    {
        private readonly IPattern[] patterns;

        public Sequence(params IPattern[] inputPatterns)
        { patterns = inputPatterns; }

        public IMatch Match(string text)
        {
            IMatch match = new Match(text, true);

            foreach (var pattern in patterns)
            {
                match = pattern.Match(match.RemainingText());

                if (!match.Success())
                {
                    return new Match(text, false);
                }
            }

            return match;
        }
    }
}