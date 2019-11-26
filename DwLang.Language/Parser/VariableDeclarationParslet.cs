using DwLang.Language.Expressions;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Var, true)]
    public class VariableDeclarationParslet : IParslet
    {
        public Expression Accept(DwLangParser parser)
        {
            parser.Take(TokenType.Var);
            var identifierToken = parser.Take(TokenType.Identifier);
            Expression initializer = default;

            if (parser.Current.Type == TokenType.Equals)
            {
                parser.Take();
                initializer = parser.ParseExpression();
            }

            return new VariableDeclaration(new Identifier(identifierToken.Text), initializer);
        }
    }
}
