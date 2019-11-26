using DwLang.Language.Expressions;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Number)]
    public class ConstantParslet : IParslet
    {
        public Expression Accept(DwLangParser parser)
        {
            var num = parser.Take(TokenType.Number).Text;
            var result = Deveel.Math.BigDecimal.Parse(num);

            return new Constant(result);
        }
    }
}
