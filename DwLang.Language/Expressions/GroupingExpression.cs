namespace DwLang.Language.Expressions
{
    public class GroupingExpression : Expression
    {
        public GroupingExpression(Expression inner)
        {
            Inner = inner;
        }

        public Expression Inner { get; }
    }
}
