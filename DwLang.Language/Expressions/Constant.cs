using Deveel.Math;

namespace DwLang.Language.Expressions
{
    public class Constant : Expression
    {
        public Constant(BigDecimal value)
        {
            Value = value;
        }

        public BigDecimal Value { get; }
    }
}
