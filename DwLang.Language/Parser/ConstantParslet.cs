using Deveel.Math;
using DwLang.Language.Expressions;
using System.Globalization;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Number)]
    public class ConstantParslet : IParslet
    {
        public Expression Accept(DwLangParser parser)
        {
            var num = parser.Take(TokenType.Number).Text;
            var result = BigDecimal.Parse(num, new NumberFormatInfo
            {
                NumberDecimalSeparator = ","
            });

            return new Constant(result);
        }
    }
}
