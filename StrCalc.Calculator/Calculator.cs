using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace StrCalc
{
    public class Calculator : ICalculator
    {
        private readonly IExpressionValidator _expressionValidator;
        private readonly List<string> _outputList = new List<string>();   // Выходная строка для реализации алгоритма преобразования в ОПЗ
        private readonly Stack<char> _stack = new Stack<char>();          // Стэк для ОПЗ

        public Calculator(IExpressionValidator expressionValidator)
        {
            _expressionValidator = expressionValidator;
        }

        public CalculationResult Calculate(string expression)
        {
            // Общая функция
            CalculationResult temporary = new CalculationResult();

            //expression = ToProcessString(expression);

            if (_expressionValidator.IsCorrect(expression))
            {
                expression = ToProcessString(expression);
                ToReversePolishNotation(expression);
                temporary = CalculationResult.FromSuccess(CalculateExpression());
            }
            else
            {
                temporary.Value = "0";
                temporary.Status = ResultType.WrongExpression;
            }
           
            return temporary;
        }

        public string ToProcessString(string expression)
        {
            const string pattern = @"\-\(\-[0-9]{1,}\)";
            expression = expression.Replace(" ", "");
            while (Regex.Match(expression, pattern) != Match.Empty)
            {
                var str = Convert.ToString(Regex.Match(expression, pattern));
                expression = expression.Replace(str!, "+" + Convert.ToString(Regex.Match(str, "[0-9]{1,}")));
            };



            //if (expression.IndexOf('-') == 0)
            //{
            //    expression = expression.Insert(0, "0");
            //}

            return expression;
        }

        public void ToReversePolishNotation(string expression)
        {
            /*
             * Функция преобразования математического выражения в форму Обратной Польской Записи - ОПЗ
             * (Reverse Polish notation, RPN), при которой математическа операция записывается после
             * двух предшествующих операднов
             */

            string str = null;  // Строковая переменная для сохранения чисел порядка 1 и более
            int stackCount;

            for (int i = 0; i < expression.Length; i++)
            {
                if (Char.IsDigit(expression[i])) 
                {
                    str += expression[i];                               // Если число - добавить к строке
                }
                else
                {
                    AddToOutputList(ref str);                          // иначе - добавить в выходную строку
                }

                if (expression[i] == '(')
                {
                    _stack.Push(expression[i]);    // Если ( - добавить в стэк
                }
                else if (expression[i] == ')')                          // Если ) :
                {
                    while (_stack.Peek() != '(')
                    {
                        _outputList.Add(Convert.ToString(_stack.Pop()));  // Пока НЕ (, выталкивать из стэка в выходную строку
                    }

                    if (_stack.Peek() == '(')
                    {
                        _stack.Pop();                                           // Вытолкнуть (
                    }
                }
                else if (!Char.IsDigit(expression[i]))                  // Если операция
                {
                    if (_stack.Count > 0)                                // пока стэк не пуст
                    {
                        stackCount = _stack.Count;
                        for (int j = 0; j < stackCount; j++)
                        {
                            if (IsNoLowerPriority(_stack.Peek(), expression[i])) // если приоритет в стэке не меньше приоритета операции в строке
                            {
                                _outputList.Add(Convert.ToString(_stack.Pop()));      // вытолкнуть из стэка в выходную строку
                            }
                        }
                    }
                    _stack.Push(expression[i]);                                          // Добавить текущий оператор в стэк
                }
            }

            AddToOutputList(ref str);                                   // Добавить в выходную строку последнее число
            stackCount = _stack.Count;

            for (int i = 0; i < stackCount; i++) _outputList.Add(Convert.ToString(_stack.Pop())); // Вытолкнуть оставшиеся операции в выходную строку
            //if (_outputList[1] == "-")
            //{
            //    _outputList.Insert(0, "0");
            //}
        }

        public bool IsNoLowerPriority(char fromStack, char fromString)
        {
            /*
             * Функция, возвращающая true в случае, когда приоритет оперции в вершине
             * стэка выше или равен приоритету опреции в строке, false в противном случае
             */

            Dictionary<char, sbyte> priority = new Dictionary<char, sbyte>
            {
                {'+', 1},
                {'-', 1},
                {'*', 2},
                {'/', 2}
            };

            if (fromStack != '(')
            {
                if (priority[fromStack] >= priority[fromString])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public string CalculateExpression()
        {
            // Функция вычисления выражения в ОПЗ

            string resultBinary;
            string pattern = "[0-9]{1,}";
            int i = 0;
            while (_outputList.Count > 1)
            {
                if (!Regex.IsMatch(_outputList[i], pattern, RegexOptions.IgnoreCase))
                {
                    if (Regex.IsMatch(_outputList[0], pattern, RegexOptions.IgnoreCase) & (_outputList[1] == "-"))
                    {
                        resultBinary = CalculateBinary(_outputList[1], "0", _outputList[0]);
                        _outputList.Insert(0, resultBinary);
                        for (int j = 0; j < 2; j++)
                        {
                            _outputList.RemoveAt(i);
                        }
                        i = 0;
                        continue;
                    }

                    resultBinary = CalculateBinary(_outputList[i], _outputList[i - 2], _outputList[i - 1]);
                    _outputList.Insert(i - 2, resultBinary);
                    for (int j = 0; j < 3; j++)
                    {
                        _outputList.RemoveAt(i - 1);
                    }
                    i = 0;
                    continue;
                }
                i++;
            }

            string result = _outputList[0];
            _outputList.RemoveAt(0);
            return result;
        }

        public string CalculateBinary(string mathOperator, string first, string second)
        {
            /*
             * Функция бинарного вычисления в зависимости от поступающего символа
             * математической операции
             */

            int intFirst = Convert.ToInt32(first);
            int intSecond = Convert.ToInt32(second);

            if (mathOperator == "+") return Convert.ToString(intFirst + intSecond);
            if (mathOperator == "-") return Convert.ToString(intFirst - intSecond);
            if (mathOperator == "*") return Convert.ToString(intFirst * intSecond);
            if (mathOperator == "/") return Convert.ToString(intFirst / intSecond);

            return null;
        }

        public void AddToOutputList(ref string str)
        {
            // Добавления числа к выходной строке

            if (str != null)
            {
                _outputList.Add(str);
                str = null;
            }
        }
    }
}
