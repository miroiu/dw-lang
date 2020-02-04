namespace DwLang.Language.Expressions
{
    public class VariableDeclarationExpression : Expression
    {
        public VariableDeclarationExpression(IdentifierExpression identifier, Expression initializer)
        {
            Identifier = identifier;
            Initializer = initializer;
        }

        public IdentifierExpression Identifier { get; }
        // May be null (e.g var x;)
        public Expression Initializer { get; }
    }
}
