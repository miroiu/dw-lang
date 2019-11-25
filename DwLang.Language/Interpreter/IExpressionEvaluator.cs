using DwLang.Language.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwLang.Language.Interpreter
{
    public interface IExpressionEvaluator
    {
        Expression Evaluate(Expression expression, ExecutionContext ctx);
    }
}
