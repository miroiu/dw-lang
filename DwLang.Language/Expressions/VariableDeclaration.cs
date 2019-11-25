namespace DwLang.Language.Expressions
{
    public class VariableDeclaration : Expression
    {
        public VariableDeclaration(Identifier identifier, Expression initializer)
        {
            Identifier = identifier;
            Initializer = initializer;
        }

        public Identifier Identifier { get; }
        // May be null (e.g var x;)
        public Expression Initializer { get; }
    }
}
