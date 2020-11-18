using System;
using System.Collections.Generic;

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

    public interface ICalculationEngine
    {
        CalculationResult Calculate(string expression);
    }

    public class AdvancedCalculationEngine : ICalculationEngine
    {
        private readonly List<CommandInfo> _supportedCommands;
        public AdvancedCalculationEngine()
        {
            _supportedCommands = BuildSupportedCommands();
        }

        public CalculationResult Calculate(string expression)
        {
            throw new System.NotImplementedException();
        }

        private List<CommandInfo> BuildSupportedCommands()
        {
            return new List<CommandInfo>
            {
                new CommandInfo(0, '+', true),
                new CommandInfo(0, '-', true),
                new CommandInfo(5, '*', true),
                new CommandInfo(5, '/', true),
                new CommandInfo(10, '!', false),
            };
        }
    }

    public class CommandInfo
    {
        public int Priority { get; set; }
        public char Symbol { get; set; } 
        public bool IsBinary { get; set; }

        public CommandInfo(int priority, char symbol, bool isBinary)
        {
            Priority = priority;
            Symbol = symbol;
            IsBinary = isBinary;
        }
    }
}
