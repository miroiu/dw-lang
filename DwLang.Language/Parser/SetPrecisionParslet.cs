using DwLang.Language.Expressions;
using DwLang.Language.Lexer;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Set, true)]
    public class SetPrecisionParslet : IParslet
    {
        public Expression Accept(DwLangParser parser)
        {
            var setToken = parser.Take(TokenType.Set);
            parser.Take(TokenType.Precision);
            var value = parser.ParseExpression();

            return new SetPrecisionExpression(value)
            {
                Token = setToken
            };
        }
    }
}
