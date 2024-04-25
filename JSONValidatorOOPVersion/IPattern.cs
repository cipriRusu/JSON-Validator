using System;

namespace JSONValidatorAlternativeVersion
{
    public interface IPattern
    {
        IMatch Match(string text);
    }
}
