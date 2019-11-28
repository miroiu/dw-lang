using DwLang.Language.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(VarArgsExpression))]
    public class VarArgsEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            var casted = expression as VarArgsExpression;
            switch (casted.OperatorType)
            {
                case VarArgsOperatorType.Avg:
                    var sumExpr = casted.Arguments.First();
                    for (var i = 1; i < casted.Arguments.Count; i++)
                    {
                        sumExpr = new BinaryExpression(sumExpr, BinaryOperatorType.Plus, casted.Arguments.ElementAt(i));
                    }

                    var binary = new BinaryExpression(sumExpr, BinaryOperatorType.Divide, new Constant(new Deveel.Math.BigDecimal(casted.Arguments.Count)));
                    return Reducer.Reduce(binary, ctx);

                case VarArgsOperatorType.Med:
                    var n = casted.Arguments.Count;

                    var args = casted.Arguments.OrderBy(e => e, new ExpressionComparer(ctx)).ToList();

                    if (args.Count % 2 == 0)
                    {
                        // even
                        var firstValue = args.ElementAt(n / 2 - 1);
                        var secondValue = args.ElementAt(n / 2);

                        var varArgs = new VarArgsExpression(VarArgsOperatorType.Avg, new Expression[] { firstValue, secondValue });
                        return Reducer.Reduce(varArgs, ctx);
                    }
                    else
                    {
                        return args.ElementAt(((n + 1) / 2) - 1);
                    }
            }
            return null;
        }
    }

    class ExpressionComparer : IComparer<Expression>
    {
        private readonly ExecutionContext _ctx;
        public ExpressionComparer(ExecutionContext ctx)
        {
            _ctx = ctx;
        }
        public int Compare(Expression x, Expression y)
        {
            var v1 = Reducer.Reduce(x, _ctx);
            var v2 = Reducer.Reduce(y, _ctx);
            if (v1 == null && v2 == null)
            {
                return 0;
            }
            if (v1 == null && v2 != null)
            {
                return -1;
            }
            if (v1 != null && v2 == null)
            {
                return 1;
            }
            var c1 = (v1 as Constant).Value;
            var c2 = (v2 as Constant).Value;
            if (c1 == null && c2 == null)
            {
                return 0;
            }
            if (c1 == null && c2 != null)
            {
                return -1;
            }
            if (c1 != null && c2 == null)
            {
                return 1;
            }
            return c1.CompareTo(c2);
        }
    }
}
