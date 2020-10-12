using System;
using System.Text.RegularExpressions;

namespace StrCalc
{
    public class ExpressionValidator : IExpressionValidator
    {
        public bool IsCorrect(string expression)
        {
            // Общая функция проверки

            if (IsCorrectSymbols(expression) & IsCorrectBrackets(expression)) return true;
            else return false;
        }

        private bool IsCorrectSymbols(string expression)
        {
            // Функция проверки вводимых символов и их порядок

            expression = expression.Replace(" ", "");       //Удаление символьных пробелов для тестов

            string pattern = 
                "^([^\\*\\/]{0,1}[\\(]{0,}[\\-]{0,1}[0-9]{1,}[\\)]{0,})(([\\+\\-\\*\\/]{1,1}[\\(]{0,}[\\-]{0,1}[0-9]{1,}[\\)]{0,}){0,})$";
            return Regex.IsMatch(expression, pattern, RegexOptions.IgnoreCase);
        }

        private bool IsCorrectBrackets(string expression)
        {
            // Функция проверки вводимых скобок

            sbyte countOpenBrack = 0;
            sbyte countCloseBrack = 0;

            foreach (char sym in expression)
            {
                if (sym == '(') countOpenBrack++;
                if (sym == ')') countCloseBrack++;
            }

            if (countOpenBrack == countCloseBrack) return true;
            else return false;
        }
    }
}