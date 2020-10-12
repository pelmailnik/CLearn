using System;
using System.Collections.Generic;
using System.Text;

namespace StrCalc
{
    public interface IExpressionValidator
    {
        bool IsCorrect(string expression);
    }
}
