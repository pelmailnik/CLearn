using System;
using System.Collections.Generic;
using System.Text;

namespace StrCalc
{
    class Addition
    {
        public string Compute(string first, string second)
        {
            return Convert.ToString(Convert.ToInt32(first) + Convert.ToInt32(second));
        }
    }
}
