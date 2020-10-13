using System;

namespace StrCalc.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            var expressionValidator = new ExpressionValidator();
            ICalculator obj = new Calculator(expressionValidator);

            while (true)
            {
                Console.Write("Enter your expression or empty line to exit: ");
                string expression = Console.ReadLine();

                if (String.IsNullOrEmpty(expression))
                {
                    break;
                }

                var result = obj.Calculate(expression);

                Console.WriteLine($"Answer: {result}");
                Console.WriteLine();
            }
        }
    }
}
