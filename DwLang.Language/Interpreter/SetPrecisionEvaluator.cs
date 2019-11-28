using DwLang.Language.Expressions;
using System;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(SetPrecision))]
    public class SetPrecisionEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            var casted = expression as SetPrecision;
            var result = ((Constant)Reducer.Reduce(casted.Precision, ctx)).Value.ToInt32();
            ctx.SetCurrentPrecision(result);
            return null;
        }
    }
}
