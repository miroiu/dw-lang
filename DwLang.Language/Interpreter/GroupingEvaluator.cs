using DwLang.Language.Expressions;
using System;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(GroupingExpression))]
    public class GroupingEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            var casted = expression as GroupingExpression;
            var result = Reducer.Reduce(casted.Inner, ctx);
            return result;
        }
    }
}
