namespace DwLang.Language.Expressions
{
    public class BinaryExpression : Expression
    {
        public BinaryExpression(Expression left, BinaryOperatorType operatorType, Expression right)
        {
            Left = left;
            OperatorType = operatorType;
            Right = right;
        }

        public Expression Left { get; }
        public BinaryOperatorType OperatorType { get; }
        public Expression Right { get; }
    }
}
