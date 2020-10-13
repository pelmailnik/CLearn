namespace StrCalc
{
    public class CalculationResult
    {
        public string Value { get; set; }
        public ResultType Status { get; set; }

        public static CalculationResult FromSuccess(string val)
        {
            return new CalculationResult
            {
                Value = val,
                Status = ResultType.Success
            };
        }

        public static CalculationResult FromError(ResultType errorType)
        {
            return new CalculationResult
            {
                Status = errorType
            };
        }

        public override string ToString()
        {
            return Status == ResultType.Success ? Value : "Format error!";
        }
    }

    public enum ResultType
    {
        Success,
        WrongExpression
    }
}