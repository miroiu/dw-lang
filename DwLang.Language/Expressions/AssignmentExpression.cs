namespace DwLang.Language.Expressions
{
    public class AssignmentExpression : Expression
    {
        public AssignmentExpression(IdentifierExpression identifier, Expression initializer)
        {
            Identifier = identifier;
            Initializer = initializer;
        }

        public IdentifierExpression Identifier { get; }
        public Expression Initializer { get; }
    }
}
