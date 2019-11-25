namespace DwLang.Language.Expressions
{
    public class SetPrecision : Expression
    {
        public SetPrecision(int precision)
        {
            Precision = precision;
        }

        public int Precision { get; }
    }
}
