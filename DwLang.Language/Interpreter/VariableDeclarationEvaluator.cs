using Deveel.Math;
using DwLang.Language.Expressions;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(VariableDeclarationExpression))]
    public class VariableDeclarationEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            var casted = expression as VariableDeclarationExpression;
            BigDecimal result = null;
            if (casted.Initializer != null)
            {
                result = ((ConstantExpression)Reducer.Reduce(casted.Initializer, ctx)).Value;
            }
            ctx.Declare(casted.Identifier.Name, result);
            return default;
        }
    }
}
