using System;

namespace JSONValidatorAlternativeVersion
{
    internal class Choice : IPattern
    {
        private IPattern[] patterns;

        public Choice(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            foreach (var pattern in patterns)
            {
                var match = pattern.Match(text);

                if (match.Success())
                {
                    return match;
                }
            }

            return new Match(text, false);
        }

        internal void Add(IPattern array)
        {
            System.Array.Resize(ref patterns, patterns.Length + 1);
            patterns[patterns.Length - 1] = array;
        }
    }
}
