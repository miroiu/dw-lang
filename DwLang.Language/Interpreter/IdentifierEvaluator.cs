using DwLang.Language.Expressions;
using System;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(Identifier))]
    public class IdentifierEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            try
            {
                var casted = expression as Identifier;
                return new Constant(ctx.Get(casted.Name));
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
