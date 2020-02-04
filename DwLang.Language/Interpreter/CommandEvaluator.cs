using DwLang.Language.Expressions;
using System;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(CommandExpression))]
    public class CommandEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            var casted = expression as CommandExpression;

            switch (casted.Type)
            {
                case CommandType.Cls:
                    ctx.Clear();
                    break;
            }

            return default;
        }
    }
}
