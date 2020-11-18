using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrCalc
{
    public static class CalculatorExtensions
    {
        public static IServiceCollection AddCalculator(this IServiceCollection services)
        {
            services
                .AddScoped<ICalculator, Calculator>()
                .AddScoped<ICalculationEngine, SimpleCalculationEngine>()
                .AddScoped<IExpressionValidator, ExpressionValidator>();

            return services;
        }
    }
}
