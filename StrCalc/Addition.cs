﻿namespace StrCalc
{
    class Addition : IMathematicalOperation
    {
        public int Compute(int first, int second)
        {
            return first + second;
        }
    }
}