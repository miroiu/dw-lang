using DwLang.Language.Expressions;
using DwLang.Language.Lexer;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Print, true)]
    public class PrintParslet : IParslet
    {
        public Expression Accept(DwLangParser parser)
        {
            var opToken = parser.Take(TokenType.Print);
            var op = opToken.ToUnaryOperatorType();
            var expr = parser.ParseExpression();

            return new UnaryExpression(op, expr)
            {
                Token = opToken
            };
        }
    }
}
