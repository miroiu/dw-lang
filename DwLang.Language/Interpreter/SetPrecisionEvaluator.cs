﻿using DwLang.Language.Expressions;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(SetPrecision))]
    public class SetPrecisionEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            var casted = expression as SetPrecision;
            var result = ((Constant)DwLangInterpreter.Evaluators[casted.Precision.GetType()].Evaluate(casted.Precision, ctx)).Value.ToInt32();
            ctx.SetCurrentPrecision(result);
            return null;
        }
    }
}