using Deveel.Math;
using DwLang.Language.Expressions;
using DwLang.Language.Lexer;
using System.Globalization;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Number)]
    public class ConstantParslet : IParslet
    {
        public Expression Accept(DwLangParser parser)
        {
            var numToken = parser.Take(TokenType.Number);
            var result = BigDecimal.Parse(numToken.Text, new NumberFormatInfo
            {
                NumberDecimalSeparator = ","
            });

            return new Constant(result)
            {
                Token = numToken
            };
        }
    }
}
