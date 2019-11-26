using DwLang.Language.Expressions;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Print, true)]
    public class PrintParslet : IParslet
    {
        public Expression Accept(DwLangParser parser)
        {
            var op = parser.Take(TokenType.Print).Type.ToUnaryOperatorType();
            var expr = parser.ParseExpression();
            return new UnaryExpression(op, expr);
        }
    }
}
