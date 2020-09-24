using System;
using Xunit;

namespace StrCalc.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("(2 + 2) - 4", 0)]
        [InlineData("(2 + (2 /2)) - 4", -1)]
        [InlineData("(2 + 2) - 4 /2", 2)]
        [InlineData("(-1)", -1)]
        public void CalculatorWorksOk(string expression, int expectedVal)
        {
            var expressionValidator = new ExpressionValidator();
            var calc = new Calculator(expressionValidator);

            var result = calc.GetResult(expression);

            Assert.Equal(expectedVal, result);
        }

        [Theory]
        [InlineData("(2 + 2) - 4", true)]
        [InlineData("(2 + 2s) - 4", false)]
        [InlineData("(-1) - 4", true)]
        public void ValidateExpressions(string expression, bool expectedResult)
        {
            var expressionValidator = new ExpressionValidator();

            var result = expressionValidator.IsIncorrect(expression);
            Assert.Equal(expectedResult, result);
        }
    }
}
