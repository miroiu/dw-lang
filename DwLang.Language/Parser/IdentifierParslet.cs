using DwLang.Language.Expressions;
using DwLang.Language.Lexer;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Identifier)]
    public class IdentifierParslet : IParslet
    {
        public Expression Accept(DwLangParser parser)
        {
            var identToken = parser.Take(TokenType.Identifier);
            return new Identifier(identToken.Text)
            {
                Token = identToken
            };
        }
    }
}
