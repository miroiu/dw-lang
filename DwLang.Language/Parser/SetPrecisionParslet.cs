using DwLang.Language.Expressions;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Set)]
    public class SetPrecisionParslet : IParslet
    {
        public Expression Accept(DwLangParser parser, Token token)
        {
            parser.Match(TokenType.Precision);
            var value = parser.ParseExpression();

            return new SetPrecision(value);
        }
    }
}
