namespace DwLang.Language.Expressions
{
    public class UnaryExpression : Expression
    {
        public UnaryExpression(UnaryOperatorType operatorType, Expression operand)
        {
            OperatorType = operatorType;
            Operand = operand;
        }

        public UnaryOperatorType OperatorType { get; }
        public Expression Operand { get; }
    }
}
