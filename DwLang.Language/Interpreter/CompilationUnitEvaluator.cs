using DwLang.Language.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(CompilationUnit))]
    public class CompilationUnitEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            var casted = expression as CompilationUnit;
            foreach (var childExpression in casted.Statements)
            {
                DwLangInterpreter.Evaluators[childExpression.GetType()].Evaluate(childExpression, ctx);
            }
            return null;
        }
    }
}
