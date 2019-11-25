using DwLang.Language.Expressions;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Identifier)]
    public class IdentifierParslet : IParslet
    {
        public Expression Accept(DwLangParser parser, Token token)
        {
            return new Identifier(token.Text);
        }
    }
}
