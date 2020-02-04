using DwLang.Language.Expressions;
using System;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(SetPrecisionExpression))]
    public class SetPrecisionEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            var casted = expression as SetPrecisionExpression;
            var result = ((ConstantExpression)Reducer.Reduce(casted.Precision, ctx)).Value.ToInt32();
            ctx.SetCurrentPrecision(result);
            return null;
        }
    }
}
