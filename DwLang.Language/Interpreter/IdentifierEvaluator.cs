using DwLang.Language.Expressions;
using System;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(Identifier))]
    public class IdentifierEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            var casted = expression as Identifier;
            var result = ctx.Get(casted.Name, expression);
            if (result == null)
            {
                throw new DwLangExecutionException($"Variable {casted.Name} is not initialized.", expression);
            }
            return new Constant(result);
        }
    }
}
