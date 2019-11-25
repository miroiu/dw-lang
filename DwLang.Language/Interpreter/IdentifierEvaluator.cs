using DwLang.Language.Expressions;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(Identifier))]
    public class IdentifierEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            var casted = expression as Identifier;
            return new Constant(ctx.Get(casted.Name));
        }
    }
}
