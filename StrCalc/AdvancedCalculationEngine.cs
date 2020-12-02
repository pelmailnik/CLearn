using System;
using System.Collections.Generic;
using System.Linq;

namespace StrCalc
{
    public class AdvancedCalculationEngine : ICalculationEngine
    {
        private readonly IExpressionValidator _expressionValidator;
        private readonly List<CommandInfo> _supportedCommands;
        private ExpressionTree<string> _tree;

        public AdvancedCalculationEngine(IExpressionValidator expressionValidator)
        {
            _supportedCommands = BuildSupportedCommands();
            _expressionValidator = expressionValidator;
        }

        public CalculationResult Calculate(string expression)
        {
            var temporary = new CalculationResult();

            if (_expressionValidator.IsCorrect(expression))
            {
                ToTree(expression);
                temporary = CalculationResult.FromSuccess(CalculateTree());
            }
            else
            {
                temporary.Value = "0";
                temporary.Status = ResultType.WrongExpression;
            }

            return temporary;
        }

        private void ToTree(string expression)
        {
            const int buf = 100;
            _tree = new ExpressionTree<string>();
            var factorOfPriority = 0;
            string str = null;

            foreach (var symbol in expression)
            {
                switch (symbol)
                {
                    case '(':
                        factorOfPriority += buf;
                        continue;
                    case ')':
                        factorOfPriority -= buf;
                        if (!string.IsNullOrEmpty(str))
                        {
                            _tree.PositionUp();
                            _tree.ToRight(str);
                        }
                        str = null;
                        continue;
                }

                if (char.IsDigit(symbol))
                {
                    str += symbol;
                    continue;
                }

                foreach (var command in _supportedCommands.Where(command => command.Symbol == symbol))
                {
                    if (_tree.IsHigherPriorityThanParent(command.Priority + factorOfPriority))
                    {
                        _tree.SetValue(Convert.ToString(symbol));
                        _tree.SetPriority(command.Priority + factorOfPriority);
                        _tree.ToLeft(str);
                        _tree.PositionUp();
                        _tree.ToRight();
                        str = null;
                    }
                    else
                    {
                        while (_tree.IsParentExists())
                        {
                            _tree.PositionUp();
                        }
                        _tree.NewParent();
                        _tree = _tree.GetHead();
                        _tree.SetValue(Convert.ToString(symbol));
                        _tree.SetPriority(command.Priority + factorOfPriority);
                        _tree.ToRight();
                    }
                }
            }

            if (string.IsNullOrEmpty(str)) return;
            _tree.PositionUp();
            _tree.ToRight(str);
        }

        private string CalculateTree()
        {
            string result = null;


            return result;
        }

        private static List<CommandInfo> BuildSupportedCommands()
        {
            return new List<CommandInfo>
            {
                new CommandInfo(1, '+', true),
                new CommandInfo(1, '-', true),
                new CommandInfo(5, '*', true),
                new CommandInfo(5, '/', true),
                new CommandInfo(10, '!', false),
            };
        }
    }
}
