using DwLang.Language.Expressions;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Print)]
    [Parslet(TokenType.Exclamation)]
    [Parslet(TokenType.Sqr)]
    public class UnaryParslet : IParslet
    {
        public Expression Accept(DwLangParser parser, Token token)
        {
            throw new System.NotImplementedException();
        }
    }
}
