using Deveel.Math;
using DwLang.Language.Expressions;
using System;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(VariableDeclaration))]
    public class VariableDeclarationEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            try
            {
                var casted = expression as VariableDeclaration;
                BigDecimal result = null;
                if (casted.Initializer != null)
                {
                    result = ((Constant)Reducer.Reduce(casted.Initializer, ctx)).Value;
                }
                ctx.Declare(casted.Identifier.Name, result);
                return null;
            }  catch (Exception e)
            {
                throw new DwLangExecutionException(e, expression);
            }
        }
    }
}
