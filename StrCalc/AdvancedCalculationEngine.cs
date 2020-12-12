using System;
using System.Collections.Generic;
using System.Linq;

namespace StrCalc
{
    public class AdvancedCalculationEngine : ICalculationEngine
    {
        private readonly IExpressionValidator _expressionValidator;
        private readonly List<CommandInfo> _supportedCommands;
        private ExpressionTree _tree;

        public AdvancedCalculationEngine(IExpressionValidator expressionValidator)
        {
            _supportedCommands = Operations.BuildSupportedCommands();
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
            _tree = new ExpressionTree();
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
                            _tree.SetValue(str);
                        }
                        continue;
                }

                if (char.IsDigit(symbol))
                {
                    str += symbol;
                    continue;
                }

                foreach (var command in _supportedCommands.Where(command => command.Symbol == symbol))
                {
                    if (command.IsBinary)
                    {
                        if (_tree.IsParentExists() && _tree.IsHigherPriorityThanParent(command.Priority + factorOfPriority))
                        {
                            _tree.SetCommand(command, factorOfPriority);
                            _tree.NewLeft(str);
                            _tree.NewRight();
                            _tree.ToRight();
                            str = null;
                        }
                        else
                        {
                            _tree.SetValue(str);
                            while (_tree.IsParentExists())
                            {
                                _tree.PositionUp();
                            }
                            _tree.NewHead();
                            _tree.PositionUp();
                            _tree = _tree.GetHead();
                            _tree.SetCommand(command, factorOfPriority);
                            _tree.NewRight();
                            _tree.ToRight();
                            str = null;
                        }
                    }
                    else
                    {
                        _tree.SetValue(str);

                        if (_tree.IsParentExists() && _tree.IsHigherPriorityThanParent(command.Priority + factorOfPriority))
                        {
                            _tree.PositionUp();
                            _tree.AddNode();
                            _tree.ToRight();
                            _tree.SetCommand(command, factorOfPriority);
                            str = null;
                        }
                        else
                        {
                            while (_tree.IsParentExists() && !_tree.IsHigherPriorityThanParent(command.Priority + factorOfPriority))
                            {
                                _tree.PositionUp();
                            }
                            //_tree.PositionUp();
                            _tree.NewHead();
                            _tree.PositionUp();
                            _tree = _tree.GetHead();
                            _tree.SetCommand(command, factorOfPriority);
                            _tree.ToLeft();


                            //while (_tree.IsParentExists() && !_tree.IsHigherPriorityThanParent(command.Priority + factorOfPriority))
                            //{
                            //    _tree.PositionUp();
                            //}

                            //if (_tree.IsParentExists())
                            //{
                            //    _tree.PositionUp();
                            //}
                            //else
                            //{
                            //    _tree.NewHead();
                            //    //_tree.ToLeft();
                            //}


                            ////_tree.PositionUp();
                            //_tree.AddNode();
                            //_tree.ToRight();
                            ////_tree.PositionUp();
                            //_tree.SetCommand(command, factorOfPriority);
                            ////_tree.NewHead();
                            //str = null;
                        }

                        
                    }
                }
            }

            if (!string.IsNullOrEmpty(str))
            {
                _tree.SetValue(str);
            }
  
            _tree.ResetPosition();
        }

        private string CalculateTree()
        {
            while (_tree.IsTreeExists())
            {
                if (_tree.InLeftCommand())
                {
                    _tree.ToLeft();
                }
                else if (_tree.InRightCommand())
                {
                    _tree.ToRight();
                }
                else
                {
                    _tree.TreeCompute(_tree.GetLeft(), _tree.GetRight());
                    if (_tree.IsParentExists())
                    {
                        _tree.PositionUp();
                    }
                }
            }

            return Convert.ToString(_tree.GetValue());
        }
    }
}
