using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Globalization;

namespace StrCalc
{
    class Calculator
    {
        private string exp;
        List<string> listOfNums = new List<string>();
        Stack<char> stack = new Stack<char>();


        public void GetExpression()
        {
            Console.WriteLine("Enter your expression:");
            do
            {
                exp = Console.ReadLine();
            } while (IsIncorrect());
        }

        private bool IsIncorrect()
        {
            if (IsIncorrectSymbols() || IsIncorrectBrackets())
            {
                Console.WriteLine("Incorrect input! Try again:");
                return true;
            }
            else return false;
        }

        private bool IsIncorrectSymbols()
        {
            string pattern = "^([^\\*\\/]{0,1}[\\(]{0,}[0-9]{1,})(([\\+\\-\\*\\/]{1,1}[\\(\\)]{0,1}[0-9]{1,}[\\)]{0,}){1,})$";
            if (Regex.IsMatch(exp, pattern, RegexOptions.IgnoreCase)) return false;
            else return true;
        }

        private bool IsIncorrectBrackets()
        {
            sbyte countOpenBrack = 0;
            sbyte countCloseBrack = 0;

            foreach (char sym in exp)
            {
                if (sym == '(') countOpenBrack++;
                if (sym == ')') countCloseBrack++;
            }

            if (countOpenBrack == countCloseBrack) return false;
            else return true;
        }

        public void ToReversePolishNotation()
        {
            string str = null;
            for (int i = 0; i < exp.Length; i++)
            {
                if (Char.IsDigit(exp[i]))
                {
                    str += exp[i];
                }
                else
                {
                    listOfNums.Add(str);
                    str = null;
                }

                if (exp[i] == '(')
                {
                    stack.Push(exp[i]);
                }
                else if (exp[i] == ')')
                {
                    while (stack.Peek() != '(')
                    {
                        listOfNums.Add(Convert.ToString(stack.Pop()));
                    }

                    stack.Pop();
                }
                else if (!Char.IsDigit(exp[i]))
                {
                    if (stack.Count > 0)
                    {
                        listOfNums.Add(Convert.ToString(stack.Pop()));
                    }
                    
                    stack.Push(exp[i]);
                }

                // TODO: Исправить преобразование в ОПЗ выражений со скобками - Stack empty
                // TODO: Исключить добавление в список '('
            }

            listOfNums.Add(str);

            for (int i = 0; i < stack.Count; i++)
            {
                listOfNums.Add(Convert.ToString(stack.Pop()));
            }

            ShowList();
        }

        public void ShowList()
        {
            Console.WriteLine("Строка:");
            foreach (var sym in listOfNums)
            {
                Console.Write("{0} ", sym);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Calculator obj = new Calculator();
            obj.GetExpression();
            obj.ToReversePolishNotation();
            
        }
    }
}
