using System;
using System.Collections.Generic;
using System.Text;

namespace JSONValidatorAlternativeVersion
{
    internal class Text : IPattern
    {
        private readonly string prefixValue;
        public Text(string prefix)
        { prefixValue = prefix; }

        public IMatch Match(string text)
        {
            return prefixValue != null 
                && text?.StartsWith(prefixValue) == true
                ? new Match(text.Substring(prefixValue.Length), true)
                : new Match(text, false);
        }
    }
}
