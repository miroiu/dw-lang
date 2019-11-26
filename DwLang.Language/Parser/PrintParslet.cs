using DwLang.Language.Expressions;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Print, true)]
    public class PrintParslet : IParslet
    {
        public Expression Accept(DwLangParser parser)
        {
            var opToken = parser.Take(TokenType.Print);
            var op = opToken.Type.ToUnaryOperatorType();
            var expr = parser.ParseExpression();

            return new UnaryExpression(op, expr)
            {
                Token = opToken
            };
        }
    }
}
