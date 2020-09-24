using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

        public int GetResult(string expression)
        {
            // Общая функция
            expression = DeleteSpaces(expression);

            GetExpression(expression);

            try
            {
                ToReversePolishNotation(expression);
                return CalculateExpression();
            }
            catch
            {

            }
        }

        private void GetExpression(string expression)
        {
            while (_expressionValidator.IsIncorrect(expression))
            {
                expression = Console.ReadLine(); // Считывть строку, пока не будет соответствовать формату
            }
        }

        private string DeleteSpaces(string expression)
        {
            // Удаление пробелов в введенной строке
            return expression.Replace(" ", "");
        }

        private void ToReversePolishNotation(string expression)
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
                if (Char.IsDigit(expression[i])) str += expression[i];  // Если число - добавить к строке
                else AddToOutputList(ref str);                          // иначе - добавить в выходную строку

                if (expression[i] == '(') _stack.Push(expression[i]);    // Если ( - добавить в стэк
                else if (expression[i] == ')')                          // Если ) :
                {
                    while (_stack.Peek() != '(') _outputList.Add(Convert.ToString(_stack.Pop()));  // Пока НЕ (, выталкивать из стэка в выходную строку
                    if (_stack.Peek() == '(') _stack.Pop();                                           // Вытолкнуть (
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
        }

        private bool IsNoLowerPriority(char fromStack, char fromString)
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
                if (priority[fromStack] >= priority[fromString]) return true;
                else return false;
            }

            return false;
        }

        private int CalculateExpression()
        {
            // Функция вычисления выражения в ОПЗ

            string result;
            string pattern = "[0-9]{1,}";
            int i = 0;
            while (_outputList.Count > 1)
            {
                if (!Regex.IsMatch(_outputList[i], pattern, RegexOptions.IgnoreCase))
                {
                    result = CalculateBinary(_outputList[i], _outputList[i - 2], _outputList[i - 1]);
                    _outputList.Insert(i - 2, result);
                    for (int j = 0; j < 3; j++) _outputList.RemoveAt(i - 1);
                    i = 0;
                    continue;
                }
                i++;
            }

            return Convert.ToInt32(_outputList[0]);
        }

        private string CalculateBinary(string mathOperator, string first, string second)
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

        private void AddToOutputList(ref string str)
        {
            // Добавления числа к выходной строке

            if (str != null)
            {
                _outputList.Add(str);
                str = null;
            }
        }

        private void ShowOutputList()
        {
            // Выводит математическое выражение в ОПЗ

            Console.WriteLine("Строка:");
            foreach (var sym in _outputList) Console.Write("{0} ", sym);
        }
    }

    public class CalculationResult
    {
        public string Value { get; set; }
        public ResultType Status { get; set; }

        public static CalculationResult FromSuccess(string val)
        {
            return new CalculationResult
            {
                Value = val,
                Status = ResultType.Success
            };
        }

        public static CalculationResult FromError(ResultType errorType)
        {
            return new CalculationResult
            {
                Status = errorType
            };
        }

        public override string ToString()
        {
            return Status == ResultType.Success ? Value : "Format error";
        }
    }

    public enum ResultType
    {
        Success,
        WrongExpression
    }

    public interface ICalculator
    {
        CalculationResult Calculate(string expression);
    }
}