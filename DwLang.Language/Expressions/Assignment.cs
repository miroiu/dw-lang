namespace DwLang.Language.Expressions
{
    public class Assignment : Expression
    {
        public Assignment(Expression identifier, Expression initializer)
        {
            Identifier = identifier;
            Initializer = initializer;
        }

        public Expression Identifier { get; }
        public Expression Initializer { get; }
    }
}
