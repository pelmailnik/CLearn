using System;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace StrCalc
{
    class Calculator
    {
        private string exp;

       
        public void GetExpression()
        {
            Console.WriteLine("Enter your expression:");
            do
            {
                exp = Console.ReadLine();
            } while (IsIncorrect());

            //Console.WriteLine(exp);
        }

        public bool IsIncorrect()
        {
            if (IsIncorrectSymbols() || IsIncorrectBrackets() == true)
            {
                Console.WriteLine("Incorrect input! Try again:");
                return true;
            }
            else return false;
        }

        public bool IsIncorrectSymbols()
        {
            string pattern = "^([^\\*\\/]{0,1}[\\(]{0,}[0-9]{1,})(([\\+\\-\\*\\/]{1,1}[\\(\\)]{0,1}[0-9][\\)]{0,}){1,})$";
            if (Regex.IsMatch(exp, pattern, RegexOptions.IgnoreCase)) return false;
            else return true;
        }

        public bool IsIncorrectBrackets()
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
    }

    class Program
    {
        static void Main(string[] args)
        {
            Calculator obj = new Calculator();
            obj.GetExpression();
            
        }
    }
}
