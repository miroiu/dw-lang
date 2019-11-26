using DwLang.Language.Expressions;

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

            return new SetPrecision(value)
            {
                Token = setToken
            };
        }
    }
}
