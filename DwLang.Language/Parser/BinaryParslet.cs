using DwLang.Language.Expressions;

namespace DwLang.Language.Parser
{
    //[Parslet(TokenType.Plus)]
    //[Parslet(TokenType.Minus)]
    //[Parslet(TokenType.X)]
    //[Parslet(TokenType.Colon)]
    //[Parslet(TokenType.Pow)]
    //[Parslet(TokenType.Prm)]
    //[Parslet(TokenType.Pwd)]
    public class BinaryParslet : IParslet
    {
        public Expression Accept(DwLangParser parser, Token token)
        {
            var operatorType = token.Type.ToBinaryOperatorType();
            return new BinaryExpression(default, operatorType, default);
        }
    }
}
