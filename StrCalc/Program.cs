using System;
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
            string pattern = "^([^\\*\\/]{0,1}[0-9]{1,})(([\\+\\-\\*\\/\\(\\)]{0,1}[0-9]){1,})$";   //победить скобка + знак
            if (Regex.IsMatch(exp, pattern, RegexOptions.IgnoreCase))
            {
                Console.WriteLine("NORM");
                return true;                                //дебаг
                //return false;                             //правильно
            }
            Console.WriteLine("Incorrect input! Try again:");
            return true;
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
