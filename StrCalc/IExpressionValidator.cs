namespace StrCalc
{
    public interface IExpressionValidator
    {
        bool IsIncorrect(string expression);
    }
}