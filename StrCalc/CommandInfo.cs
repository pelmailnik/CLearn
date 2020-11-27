namespace StrCalc
{
    public class CommandInfo
    {
        public int Priority { get; set; }
        public char Symbol { get; set; } 
        public bool IsBinary { get; set; }

        public CommandInfo(int priority, char symbol, bool isBinary)
        {
            Priority = priority;
            Symbol = symbol;
            IsBinary = isBinary;
        }
    }
}
