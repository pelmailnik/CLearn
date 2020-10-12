using System;
using Xunit;

namespace StrCalc.Test
{
    public class CalculatorTest
    {
        [Theory]
        [InlineData("(2 + 2) - 4", 0)]
        [InlineData("(2 + (2 /2)) - 4", -1)]
        [InlineData("(2 + 2) - 4 /2", 2)]
        [InlineData("(-1)", -1)]
        [InlineData("-1", -1)]
        [InlineData("4-(-5)", 9)]
        [InlineData("-4-(-5)", 1)]
        [InlineData("(-4)-(-5)", 1)]
        [InlineData("(-4-5)", -9)]
        public void CalculatorWorksOk(string expression, int expectedVal)
        {
            var expressionValidator = new ExpressionValidator();
            var calc = new Calculator(expressionValidator);

            var result = calc.Calculate(expression);

            Assert.Equal(Convert.ToString(expectedVal), Convert.ToString(result));
        }
    }
}