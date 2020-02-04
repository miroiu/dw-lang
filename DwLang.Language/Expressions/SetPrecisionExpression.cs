namespace DwLang.Language.Expressions
{
    public class SetPrecisionExpression : Expression
    {
        public SetPrecisionExpression(Expression precision)
        {
            Precision = precision;
        }

        public Expression Precision { get; }
    }
}
