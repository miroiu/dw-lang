using DwLang.Language.Expressions;
using System;

namespace DwLang.Language.Interpreter
{
    public static class Reducer
    {
        public static Expression Reduce(Expression expression, ExecutionContext ctx)
        {
            try
            {
                if (expression == null || expression is ConstantExpression)
                {
                    return expression;
                }
                return Reduce(DwLangInterpreter.Evaluators[expression.GetType()].Evaluate(expression, ctx), ctx);
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
