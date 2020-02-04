using Deveel.Math;

namespace DwLang.Language.Expressions
{
    public class ConstantExpression : Expression
    {
        public ConstantExpression(BigDecimal value)
        {
            Value = value;
        }

        public BigDecimal Value { get; }
    }
}
