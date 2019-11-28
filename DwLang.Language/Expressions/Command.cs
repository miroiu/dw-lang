namespace DwLang.Language.Expressions
{
    public class Command : Expression
    {
        public Command(CommandType command)
        {
            Type = command;
        }

        public CommandType Type { get; }
    }
}
