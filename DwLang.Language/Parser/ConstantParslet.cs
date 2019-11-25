using DwLang.Language.Expressions;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Number)]
    public class ConstantParslet : IParslet
    {
        public Expression Accept(DwLangParser parser, Token token)
        {
            var result = Deveel.Math.BigDecimal.Parse(token.Text);

            return new Constant(result);
        }
    }
}
