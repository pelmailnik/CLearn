using System;

namespace StrCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            var expressionValidator = new ExpressionValidator();
            ICalculator calculator = new Calculator(expressionValidator);

            while (true)
            {
                Console.Write("Enter your expression or empty line to exit: ");
                var expression = Console.ReadLine();

                if (string.IsNullOrEmpty(expression))
                {
                    break;
                }

                var result = calculator.Calculate(expression);

                Console.WriteLine($"Answer: {result}");
                Console.WriteLine();
            }
        }
    }
}
