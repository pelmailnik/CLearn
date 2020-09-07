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

        public void ProcessString()
        {
            string str = null;
            for (int i = 0; i < exp.Length; i++)
            {
                if (Char.IsDigit(exp[i]))
                {
                    str += exp[i];

                }

                if (!Char.IsDigit(exp[i]))
                {
                    if (str != null)
                    {
                        listOfNums.Add(str);
                    }

                    str = null;
                    listOfNums.Add(Convert.ToString(exp[i]));
                }
            }

            listOfNums.Add(str);
        }

        public void Calculate()
        {
            string value;

            do
            {
                int LastOpen = 0;
                int FirstClose = 0;

                for (int i = 0; i < listOfNums.Count; i++)
                {
                    if (listOfNums[i] == "(") LastOpen = i;
                }

                for (int i = 0; i < listOfNums.Count; i++)
                {
                    if (listOfNums[i] == ")")
                    {
                        FirstClose = i;
                        break;
                    }
                }

                value = Convert.ToString(ToCalculateBinary(LastOpen, FirstClose));
                RemoveFromList(LastOpen, FirstClose);
                listOfNums.Insert(LastOpen, value);
                //ShowList();
                if ((LastOpen == 0) && (FirstClose == 0)) break;

            } while (true);

            Console.Write("value: {0}", value);
        }

        public void ShowList()
        {
            foreach (var el in listOfNums)
            {
                Console.Write("{0} ", el);
            }
        }

        public void RemoveFromList(int LastOpen, int FirstClose)
        {
            if ((LastOpen != 0) && (FirstClose != 0))
            {
                for (int i = FirstClose; i >= LastOpen; i--)
                {
                    listOfNums.RemoveAt(i);
                }
            }
            
        }

        public int ToCalculateBinary(int bracketOpen, int bracketClose)
        {
            int result = 0;
            if (listOfNums[bracketOpen + 2] == "+")
            {
                result = Convert.ToInt32(listOfNums[bracketOpen + 1]) + Convert.ToInt32(listOfNums[bracketClose - 1]);
            }
            else if (listOfNums[bracketOpen + 2] == "-")
            {
                result = Convert.ToInt32(listOfNums[bracketOpen + 1]) - Convert.ToInt32(listOfNums[bracketClose - 1]);
            }
            else if (listOfNums[bracketOpen + 2] == "*")
            {
                result = Convert.ToInt32(listOfNums[bracketOpen + 1]) * Convert.ToInt32(listOfNums[bracketClose - 1]);
            }
            else if (listOfNums[bracketOpen + 2] == "/")
            {
                result = Convert.ToInt32(listOfNums[bracketOpen + 1]) / Convert.ToInt32(listOfNums[bracketClose - 1]);
            }
            else
            {
                if (listOfNums[1] == "+")
                {
                    result = Convert.ToInt32(listOfNums[0]) + Convert.ToInt32(listOfNums[2]);
                }
                if (listOfNums[bracketOpen + 1] == "-")
                {
                    result = Convert.ToInt32(listOfNums[0]) - Convert.ToInt32(listOfNums[2]);
                }
                if (listOfNums[bracketOpen + 1] == "*")
                {
                    result = Convert.ToInt32(listOfNums[0]) * Convert.ToInt32(listOfNums[2]);
                }
                if (listOfNums[bracketOpen + 1] == "/")
                {
                    result = Convert.ToInt32(listOfNums[0]) / Convert.ToInt32(listOfNums[2]);
                }
            }

            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Calculator obj = new Calculator();
            obj.GetExpression();
            obj.ProcessString();
            obj.Calculate();
        }
    }
}
