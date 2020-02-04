using DwLang.Language.Expressions;
using DwLang.Language.Lexer;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Var, true)]
    public class VariableDeclarationParslet : IParslet
    {
        public Expression Accept(DwLangParser parser)
        {
            var varToken = parser.Take(TokenType.Var);
            var identifierToken = parser.Take(TokenType.Identifier);
            Expression initializer = default;

            if (parser.Current.Type == TokenType.Equals)
            {
                parser.Take();
                initializer = parser.ParseExpression();
            }

            return new VariableDeclarationExpression(new IdentifierExpression(identifierToken.Text)
            {
                Token = identifierToken
            }, initializer)
            {
                Token = varToken
            };
        }
    }
}
