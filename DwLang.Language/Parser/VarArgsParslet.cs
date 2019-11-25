using DwLang.Language.Expressions;
using System.Collections.Generic;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Avg)]
    [Parslet(TokenType.Med)]
    public class VarArgsParslet : IParslet
    {
        public Expression Accept(DwLangParser parser, Token token)
        {
            var operatorType = token.Type == TokenType.Med ? VarArgsOperatorType.Med : VarArgsOperatorType.Avg;

            List<Expression> args = new List<Expression>();

            Token peek = parser.Peek();
            while (peek.Type != TokenType.Semicolon)
            {
                args.Add(parser.ParseExpression());
            }

            return new VarArgsExpression(operatorType, args.ToArray());
        }
    }
}
