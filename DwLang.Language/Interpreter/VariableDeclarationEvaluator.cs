using Deveel.Math;
using DwLang.Language.Expressions;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(VariableDeclaration))]
    public class VariableDeclarationEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            var casted = expression as VariableDeclaration;
            BigDecimal result = null;
            if (casted.Initializer != null)
            {
                result = ((Constant)DwLangInterpreter.Evaluators[casted.Initializer.GetType()].Evaluate(casted.Initializer, ctx)).Value;
            }
            ctx.Declare(casted.Identifier.Name, result);
            return null;
        }
    }
}
