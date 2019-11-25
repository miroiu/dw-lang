using DwLang.Language.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(Assignment))]
    public class AssignmentEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            var casted = expression as Assignment;
            var result = (Constant)DwLangInterpreter.Evaluators[casted.Initializer.GetType()].Evaluate(casted.Initializer, ctx);
            ctx.Assign(casted.Identifier.Name, result.Value);
            return null;
        }
    }
}
