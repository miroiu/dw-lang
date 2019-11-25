namespace DwLang.Language.Expressions
{
    public class SetPrecision : Expression
    {
        public SetPrecision(Expression precision)
        {
            Precision = precision;
        }

        public Expression Precision { get; }
    }
}
