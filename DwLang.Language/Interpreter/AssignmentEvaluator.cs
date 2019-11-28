using DwLang.Language.Expressions;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(Assignment))]
    public class AssignmentEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            var casted = expression as Assignment;
            var result = (Constant)Reducer.Reduce(casted.Initializer, ctx);
            ctx.Assign(casted.Identifier.Name, result.Value);
            return null;
        }
    }
}
