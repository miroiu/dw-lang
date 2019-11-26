using DwLang.Language.Expressions;
using System.Collections.Generic;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Avg)]
    [Parslet(TokenType.Med)]
    public class VarArgsParslet : IParslet
    {
        public Expression Accept(DwLangParser parser)
        {
            var operatorType = parser.Take().Type == TokenType.Med ? VarArgsOperatorType.Med : VarArgsOperatorType.Avg;

            List<Expression> args = new List<Expression>();

            while (!parser.IsEndOfStatement())
            {
                args.Add(parser.ParseExpression());
            }

            return new VarArgsExpression(operatorType, args.ToArray());
        }
    }
}
