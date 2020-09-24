using System;
using System.Text.RegularExpressions;

namespace StrCalc
{
    public class ExpressionValidator : IExpressionValidator
    {
        public bool IsIncorrect(string expression)
        {
            // Общая функция проверки

            if (IsIncorrectSymbols(expression) || IsIncorrectBrackets(expression))
            {
                Console.WriteLine("Incorrect input! Try again:");
                return true;
            }

            return false;
        }

        private bool IsIncorrectSymbols(string expression)
        {
            // Функция проверки вводимых символов и их порядок

            const string pattern = "^([^\\*\\/]{0,1}[\\(]{0,}[0-9]{1,})(([\\+\\-\\*\\/]{1,1}[\\(\\)]{0,1}[0-9]{1,}[\\)]{0,}){1,})$";
            if (Regex.IsMatch(expression, pattern, RegexOptions.IgnoreCase))
                return false;

            return true;
        }

        private bool IsIncorrectBrackets(string expression)
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
    }
}