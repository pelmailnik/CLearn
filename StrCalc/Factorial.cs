using System;
using System.Collections.Generic;
using System.Text;

namespace StrCalc
{
    class Factorial : IMathematicalOperation
    {
        public int Compute(int first)
        {
            if (first == 1)
            {
                return 1;
            }
            return first * Compute(first - 1);
        }

        public int Compute(int first, int second)
        {
            throw new NotImplementedException();
        }
    }
}