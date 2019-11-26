using DwLang.Language.Expressions;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Identifier)]
    public class IdentifierParslet : IParslet
    {
        public Expression Accept(DwLangParser parser)
        {
            var ident = parser.Take(TokenType.Identifier).Text;
            return new Identifier(ident);
        }
    }
}
