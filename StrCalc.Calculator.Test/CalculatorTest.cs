using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace StrCalc.Test
{
    public class CalculatorTest
    {
        [Theory]
        [InlineData("1 + 2 + 3 + 4", 10)]
        [InlineData("1 - 2 + 3 - 4", -2)]
        [InlineData("(2 + 2) - 4", 0)]
        [InlineData("(2 + (2 /2)) - 4", -1)]
        [InlineData("(2 + 2) - 4 /2", 2)]
        [InlineData("(-1)", -1)]
        [InlineData("-1", -1)]
        [InlineData("4-(-5)", 9)]
        [InlineData("-4-(-5)", 1)]
        [InlineData("(-4)-(-5)", 1)]
        [InlineData("(-4-5)", -9)]
        [InlineData("(-4)+(-5)", -9)]
        [InlineData("(2 + (2 *2)) - 4", 2)]
        [InlineData("(2 + 2) - 4 *2", -4)]
        public void CalculatorWorksOk(string expression, int expectedVal)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddCalculator();

            var serviceProvider = services.BuildServiceProvider();

            ICalculator calc = serviceProvider.GetRequiredService<ICalculator>();

            var result = calc.Calculate(expression);

            Assert.Equal(Convert.ToString(expectedVal), Convert.ToString(result));
        }
    }
}