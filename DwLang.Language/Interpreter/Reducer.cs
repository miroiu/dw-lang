using DwLang.Language.Expressions;

namespace DwLang.Language.Interpreter
{
    public static class Reducer
    {
        public static Expression Reduce(Expression e, ExecutionContext ctx)
        {
            if (e == null || e is Constant)
            {
                return e;
            }
            return Reduce(DwLangInterpreter.Evaluators[e.GetType()].Evaluate(e, ctx), ctx);
        }
    }
}
