namespace DwLang.Language.Expressions
{
    public class Grouping : Expression
    {
        public Grouping(Expression inner)
        {
            Inner = inner;
        }

        public Expression Inner { get; }
    }
}
