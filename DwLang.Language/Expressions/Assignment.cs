namespace DwLang.Language.Expressions
{
    public class Assignment : Expression
    {
        public Assignment(Identifier identifier, Expression initializer)
        {
            Identifier = identifier;
            Initializer = initializer;
        }

        public Identifier Identifier { get; }
        public Expression Initializer { get; }
    }
}
