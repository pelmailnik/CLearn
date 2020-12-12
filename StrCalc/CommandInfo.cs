namespace StrCalc
{
    public class CommandInfo
    {
        public int Priority { get; set; }
        public char Symbol { get; set; } 
        public bool IsBinary { get; set; }
        public IMathematicalOperation MathematicalOperation { get; set; }

        public CommandInfo(int priority, char symbol, bool isBinary, IMathematicalOperation mathOperation)
        {
            Priority = priority;
            Symbol = symbol;
            IsBinary = isBinary;
            MathematicalOperation = mathOperation;
        }

        public int Compute(int first, int second)
        {
            return MathematicalOperation.Compute(first, second);
        }

        public int Compute(int first)
        {
            return MathematicalOperation.Compute(first);
        }
    }
}
