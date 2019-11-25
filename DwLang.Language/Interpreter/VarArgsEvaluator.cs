using DwLang.Language.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(VarArgsExpression))]
    public class VarArgsEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            var casted = expression as VarArgsExpression;
            switch(casted.OperatorType)
            {
                case VarArgsOperatorType.Avg:
                    var sumExpr = casted.Arguments.First();
                    for (var i = 1; i < casted.Arguments.Count; i++)
                    {
                        sumExpr = new BinaryExpression(sumExpr, BinaryOperatorType.Plus, casted.Arguments.ElementAt(i));
                    }
                    return new BinaryExpression(sumExpr, BinaryOperatorType.Divide, new Constant(new Deveel.Math.BigDecimal(casted.Arguments.Count)));

                case VarArgsOperatorType.Med:
                    var n = casted.Arguments.Count;
                    if (casted.Arguments.Count % 2 == 0)
                    {
                        // even
                        var firstValue = casted.Arguments.ElementAt(n / 2 - 1);
                        var secondValue = casted.Arguments.ElementAt((n + 1) / 2 - 1);
                        return new VarArgsExpression(VarArgsOperatorType.Avg, new Expression[] {firstValue, secondValue});
                    } else
                    {
                        return casted.Arguments.ElementAt(((n + 1)/2) - 1);
                    }
            }
            return null;
        }
    }
}
