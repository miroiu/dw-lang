using DwLang.Language.Expressions;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(Constant))]
    public class ConstantEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            return expression;
        }
    }
}
