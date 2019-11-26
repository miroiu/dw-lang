using DwLang.Language.Expressions;

namespace DwLang.Language.Parser
{
    // Random convention for the binary parser
    [Parslet(TokenType.X)]
    public class BinaryParslet : IParslet
    {
        public Expression Accept(DwLangParser parser)
            => ParseBinaryExpression(parser);

        private Expression ParseBinaryExpression(DwLangParser parser, Expression left = default, OperatorPrecedence parentPrecedence = OperatorPrecedence.None)
        {
            if (left == default)
            {
                // No need for unary parslet
                if (parser.Current.Type.IsUnaryOperator())
                {
                    var precedence = parser.Current.Type.ToOperatorPrecedence();
                    var operatorType = parser.Take().Type.ToUnaryOperatorType();
                    left = new UnaryExpression(operatorType, ParseBinaryExpression(parser, left, precedence));
                }
                else
                {
                    left = parser.ParsePrimaryExpression();

                    if (parser.Current.Type == TokenType.Exclamation)
                    {
                        var operatorType = parser.Take().Type.ToUnaryOperatorType();
                        left = new UnaryExpression(operatorType, left);
                    }
                }
            }

            while (!parser.IsEndOfStatement())
            {
                if (parser.Current.Type.IsBinaryOperator())
                {
                    var precedence = parser.Current.Type.ToOperatorPrecedence();
                    if (parentPrecedence >= precedence)
                    {
                        return left;
                    }

                    var operatorType = parser.Take().Type.ToBinaryOperatorType();
                    left = new BinaryExpression(left, operatorType, ParseBinaryExpression(parser, parentPrecedence: precedence));
                }
                else
                {
                    return left;
                }
            }

            return left;
        }
    }
}
