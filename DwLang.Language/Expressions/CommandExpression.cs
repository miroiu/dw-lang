namespace DwLang.Language.Expressions
{
    public class CommandExpression : Expression
    {
        public CommandExpression(CommandType command)
        {
            Type = command;
        }

        public CommandType Type { get; }
    }
}
