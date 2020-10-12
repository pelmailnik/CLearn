namespace StrCalc
{
    public interface ICalculator
    {
        public CalculationResult Calculate(string expression);
        string ToProcessString(string expression);
        void ToReversePolishNotation(string expression);
        bool IsNoLowerPriority(char fromStack, char fromString);
        string CalculateExpression();
        string CalculateBinary(string mathOperator, string first, string second);
        void AddToOutputList(ref string str);
    }
}