using System;

namespace StrCalc
{
    class Division : IMathematicalOperation
    {
        public int Compute(int first, int second)
        {
            return first / second;
        }

        public int Compute(int first)
        {
            throw new NotImplementedException();
        }
    }
}
