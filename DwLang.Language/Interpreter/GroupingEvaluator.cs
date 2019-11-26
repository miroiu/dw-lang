using DwLang.Language.Expressions;
using System;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(Grouping))]
    public class GroupingEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            try
            {
                var casted = expression as Grouping;
                var result = Reducer.Reduce(casted.Inner, ctx);
                return result;
            }
            catch (Exception e)
            {
                if (e is DwLangExecutionException)
                {
                    throw e;
                }
                throw new DwLangExecutionException(e, expression);
            }
        }
    }
}
