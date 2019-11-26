using DwLang.Language.Expressions;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Print, true)]
    [Parslet(TokenType.Exclamation)]
    [Parslet(TokenType.Sqr)]
    public class UnaryParslet : IParslet
    {
        public Expression Accept(DwLangParser parser, Token token)
        {
            var expr = parser.ParseExpression(OperatorPrecedence.Prefix);
            return new UnaryExpression(token.Type.ToUnaryOperatorType(), expr);
        }
    }
}
