using DwLang.Language.Expressions;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(AssignmentExpression))]
    public class AssignmentEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            var casted = expression as AssignmentExpression;
            var result = (ConstantExpression)Reducer.Reduce(casted.Initializer, ctx);
            ctx.Assign(casted.Identifier.Name, result.Value);
            return null;
        }
    }
}
