using System;

namespace StrCalc
{
    public class Calculator : ICalculator
    {
        private readonly ICalculationEngine _engine;

        public Calculator(ICalculationEngine engine)
        {
            _engine = engine;
        }

        public CalculationResult Calculate(string expression)
        {
            return _engine.Calculate(expression);
        }
    }
}
