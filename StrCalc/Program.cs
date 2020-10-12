using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace StrCalc
{
    class Calculator
    {
        private string expression;
        List<string> outputList = new List<string>();   // Выходная строка для реализации алгоритма преобразования в ОПЗ
        Stack<char> stack = new Stack<char>();          // Стэк для ОПЗ

        public Calculator(string expression)
        {
            this.expression = DeleteSpases(expression);
        }

        private void GetExpression()
        {
            while (IsIncorrect()) expression = Console.ReadLine(); // Считывть строку, пока не будет соответствовать формату
        }

        private string DeleteSpases(string expression)
        {
            // Удаление пробелов в введенной строке

            string str = null;
            foreach (char sym in expression)
            {
                if (sym != ' ') str += Convert.ToString(sym);
            }
            return str;
        }

        private bool IsIncorrect()
        {
            // Общая функция проверки

            if (IsIncorrectSymbols() || IsIncorrectBrackets())
            {
                Console.WriteLine("Incorrect input! Try again:");
                return true;
            }
            else return false;
        }

        private bool IsIncorrectSymbols()
        {
            // Функция проверки вводимых символов и их порядок

            string pattern = "^([^\\*\\/]{0,1}[\\(]{0,}[0-9]{1,})(([\\+\\-\\*\\/]{1,1}[\\(\\)]{0,1}[0-9]{1,}[\\)]{0,}){1,})$";
            if (Regex.IsMatch(expression, pattern, RegexOptions.IgnoreCase)) return false;
            else return true;
        }

        private bool IsIncorrectBrackets()
        {
            // Функция проверки вводимых скобок

            sbyte countOpenBrack = 0;
            sbyte countCloseBrack = 0;

            foreach (char sym in expression)
            {
                if (sym == '(') countOpenBrack++;
                if (sym == ')') countCloseBrack++;
            }

            if (countOpenBrack == countCloseBrack) return false;
            else return true;
        }

        private void ToReversePolishNotation()
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

                if (expression[i] == '(') stack.Push(expression[i]);    // Если ( - добавить в стэк
                else if (expression[i] == ')')                          // Если ) :
                {
                    while (stack.Peek() != '(') outputList.Add(Convert.ToString(stack.Pop()));  // Пока НЕ (, выталкивать из стэка в выходную строку
                    if (stack.Peek() == '(') stack.Pop();                                           // Вытолкнуть (
                }
                else if (!Char.IsDigit(expression[i]))                  // Если операция
                {
                    if (stack.Count > 0)                                // пока стэк не пуст
                    {
                        stackCount = stack.Count;
                        for (int j = 0; j < stackCount; j++)
                        {
                            if (IsNoLowerPriority(stack.Peek(), expression[i])) // если приоритет в стэке не меньше приоритета операции в строке
                            {
                                outputList.Add(Convert.ToString(stack.Pop()));      // вытолкнуть из стэка в выходную строку
                            }
                        }
                    }
                    stack.Push(expression[i]);                                          // Добавить текущий оператор в стэк
                }
            }

            AddToOutputList(ref str);                                   // Добавить в выходную строку последнее число
            stackCount = stack.Count;

            for (int i = 0; i < stackCount; i++) outputList.Add(Convert.ToString(stack.Pop())); // Вытолкнуть оставшиеся операции в выходную строку
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
            while (outputList.Count > 1)
            {
                if (!Regex.IsMatch(outputList[i], pattern, RegexOptions.IgnoreCase))
                {
                    result = CalculateBinary(outputList[i], outputList[i - 2], outputList[i - 1]);
                    outputList.Insert(i - 2, result);
                    for (int j = 0; j < 3; j++) outputList.RemoveAt(i - 1);
                    i = 0;
                    continue;
                }
                i++;
            }

            return Convert.ToInt32(outputList[0]);
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
                outputList.Add(str);
                str = null;
            }
        }

        private void ShowOutputList()
        {
            // Выводит математическое выражение в ОПЗ

            Console.WriteLine("Строка:");
            foreach (var sym in outputList) Console.Write("{0} ", sym);
        }

        public int GetResult()
        {
            // Общая функция

            GetExpression();
            ToReversePolishNotation();
            return CalculateExpression();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string expression;

            Console.Write("Enter your expression: ");
            expression = Console.ReadLine();

            Calculator obj = new Calculator(expression);

            Console.Write("Answer: ");
            Console.WriteLine(obj.GetResult());
        }
    }
}
