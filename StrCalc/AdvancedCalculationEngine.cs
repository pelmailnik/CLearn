using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StrCalc
{
    public class AdvancedCalculationEngine : ICalculationEngine
    {
        private readonly IExpressionValidator _expressionValidator;
        private readonly List<CommandInfo> _supportedCommands;
        private ExpressionTree<string> tree;

        public AdvancedCalculationEngine(IExpressionValidator expressionValidator)
        {
            _supportedCommands = BuildSupportedCommands();
            _expressionValidator = expressionValidator;
        }

        public CalculationResult Calculate(string expression)
        {
            CalculationResult temporary = new CalculationResult();

            //expression = ToProcessString(expression);

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
            tree = new ExpressionTree<string>();
            int coefOfPriority = 0;
            int counter = 0;
            string pattern = "^[0-9]+$";

            while (expression.Length > 0)
            {
                foreach (CommandInfo command in _supportedCommands)
                {
                    if (command.Symbol == expression[counter])
                    {
                        string tmp = System.Convert.ToString(expression[counter]);
                        tree.AddCommandAndLeft(tmp, expression.Substring(0, counter), command.Priority + coefOfPriority);
                        expression = expression.Substring(counter + 1);
                        counter = 0;
                    }
                }

                if (Regex.IsMatch(expression, pattern, RegexOptions.IgnoreCase))
                {
                    tree.AddRight(expression);
                    break;
                }

                counter++;
            }
        }

        private string CalculateTree()
        {
            string result = null;


            return result;
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
}
