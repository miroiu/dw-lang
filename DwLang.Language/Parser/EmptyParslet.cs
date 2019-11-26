using DwLang.Language.Expressions;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Semicolon, true)]
    [Parslet(TokenType.EndOfCode, true)]
    public class EmptyParslet : IParslet
    {
        public Expression Accept(DwLangParser parser)
        {
            return new EmptyExpression();
        }
    }
}
