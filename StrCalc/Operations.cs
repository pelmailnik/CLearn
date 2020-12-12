using System.Collections.Generic;

namespace StrCalc
{
    class Operations
    {
        public static List<CommandInfo> BuildSupportedCommands()
        {
            return new List<CommandInfo>
            {
                new CommandInfo(1, '+', true, new Addition()),
                new CommandInfo(1, '-', true, new Substraction()),
                new CommandInfo(5, '*', true, new Multiplication()),
                new CommandInfo(5, '/', true, new Division()),
                new CommandInfo(10, '!', false, new Factorial()),
            };
        }
    }
}
