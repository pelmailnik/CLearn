using System;
using Xunit;

namespace StrCalc.Test
{
    public class ValidateTest
    {
        [Theory]
        [InlineData("(2 + 2) - 4", true)]
        [InlineData("(2+2s)-4", false)]
        [InlineData("(-1) - 4", true)]
        [InlineData("(1) - 4", true)]
        [InlineData("4", true)]
        [InlineData("-5", true)]
        [InlineData("-5+8", true)]
        [InlineData("4--(5)", false)]
        [InlineData("4-(-5)", true)]

        private void ValidateExpressions(string expression, bool expectedResult)
        {
            var expressionValidator = new ExpressionValidator();

            var result = expressionValidator.IsCorrect(expression);
            Assert.Equal(expectedResult, result);
        }
    }
}
