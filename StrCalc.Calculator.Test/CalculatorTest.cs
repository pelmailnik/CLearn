using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace StrCalc.Test
{
    public class CalculatorTest
    {
        protected IServiceCollection Services;
        protected ServiceProvider ServiceProvider;
        protected ICalculator Calc;
        //protected CalculationResult Result;

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
        [InlineData("2 + 2!", 4)]
        [InlineData("(2 + 2) - 4!", -20)]
        [InlineData("(1 + 2) + 3!", 9)]
        [InlineData("1 + (2 + 3)!", 121)]
        [InlineData("1 + ((2 + 3)!) - 100", 21)]
        [InlineData("(1 + (2 + 3))! - 100", 620)]
        [InlineData("(1 + 2)! - 3", 3)]

        public void CalculatorWorksOk(string expression, int expectedVal)
        {
            Services = new ServiceCollection();
            Services.AddCalculator();

            ServiceProvider = Services.BuildServiceProvider();

            Calc = ServiceProvider.GetRequiredService<ICalculator>();

            var result = Calc.Calculate(expression);

            Assert.Equal(Convert.ToString(expectedVal), Convert.ToString(result));
        }
    }
}