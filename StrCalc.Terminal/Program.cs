using Microsoft.Extensions.DependencyInjection;
using System;

namespace StrCalc.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddCalculator();

            var serviceProvider = services.BuildServiceProvider();

            ICalculator obj = serviceProvider.GetRequiredService<ICalculator>();

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
