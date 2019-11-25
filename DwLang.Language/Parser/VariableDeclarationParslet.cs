using DwLang.Language.Expressions;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Var, true)]
    public class VariableDeclarationParslet : IParslet
    {
        public Expression Accept(DwLangParser parser, Token token)
        {
            var identifierToken = parser.Match(TokenType.Identifier);
            Expression initializer = default;

            var peek = parser.Peek();
            if (peek.Type == TokenType.Equals)
            {
                parser.Take();
                initializer = parser.ParsePrimaryExpression();
            }

            return new VariableDeclaration(new Identifier(identifierToken.Text), initializer);
        }
    }
}
