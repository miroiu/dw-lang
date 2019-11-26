using DwLang.Language.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwLang.Language.Interpreter
{
    public static class Reducer
    {
        public static Expression Reduce(Expression e, ExecutionContext ctx)
        {
            try
            {
                if (e == null || e is Constant)
                {
                    return e;
                }
                return Reduce(DwLangInterpreter.Evaluators[e.GetType()].Evaluate(e, ctx), ctx);
            } catch (Exception ex)
            {
                throw new DwLangExecutionException(ex, e);
            }
        }
    }
}
