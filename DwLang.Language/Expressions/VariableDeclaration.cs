namespace DwLang.Language.Expressions
{
    public class VariableDeclaration : Expression
    {
        public VariableDeclaration(Expression identifier, Expression initializer)
        {
            Identifier = identifier;
            Initializer = initializer;
        }

        public Expression Identifier { get; }
        // May be null (e.g var x;)
        public Expression Initializer { get; }
    }
}
