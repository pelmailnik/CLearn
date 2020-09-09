using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Globalization;

namespace StrCalc
{
    class Calculator
    {
        private string exp;
        List<string> outputList = new List<string>();
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
            int stackCount;

            for (int i = 0; i < exp.Length; i++)
            {
                if (Char.IsDigit(exp[i])) str += exp[i];
                else AddToOutputList(ref str);

                if (exp[i] == '(') stack.Push(exp[i]);
                else if (exp[i] == ')')
                {
                    while (stack.Peek() != '(') outputList.Add(Convert.ToString(stack.Pop()));
                    if (stack.Peek() == '(') stack.Pop();
                }
                else if (!Char.IsDigit(exp[i]))
                {
                    if ((stack.Count > 0))
                    {
                        stackCount = stack.Count;
                        for (int j = 0; j < stackCount; j++)
                        {
                            if (IsNoLowerPriority(stack.Peek(), exp[i]))
                            {
                                outputList.Add(Convert.ToString(stack.Pop()));
                            }
                        }
                    }
                    stack.Push(exp[i]);
                }
            }

            AddToOutputList(ref str);
            stackCount = stack.Count;

            for (int i = 0; i < stackCount; i++) outputList.Add(Convert.ToString(stack.Pop()));

            ShowOutputList();
        }

        public bool IsNoLowerPriority(char fromStack, char fromString)
        {
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

            //if (fromStack != '(')
            //{
            //    if ((fromStack == '+') && (fromString == '*')) return false;
            //    else if ((fromStack == '*') && (fromString == '+')) return true;
            //    else return true;
            //}
            //else return false;
        }

        public void AddToOutputList(ref string str)
        {
            if (str != null)
            {
                outputList.Add(str);
                str = null;
            }
        }

        public void ShowOutputList()
        {
            Console.WriteLine("Строка:");
            foreach (var sym in outputList) Console.Write("{0} ", sym);
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
