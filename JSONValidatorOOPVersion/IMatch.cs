using System;
using System.Collections.Generic;
using System.Text;

namespace JSONValidatorAlternativeVersion
{
    public interface IMatch
    {
        bool Success();
        string RemainingText();
    }
}
