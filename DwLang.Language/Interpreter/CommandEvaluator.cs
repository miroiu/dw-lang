using DwLang.Language.Expressions;
using System;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(Command))]
    public class CommandEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            var casted = expression as Command;

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
