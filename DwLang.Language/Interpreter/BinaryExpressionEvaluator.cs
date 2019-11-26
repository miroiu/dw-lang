using Deveel.Math;
using DwLang.Language.Expressions;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(BinaryExpression))]
    public class BinaryExpressionEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            var casted = expression as BinaryExpression;
            var left = (Reducer.Reduce(casted.Left, ctx) as Constant).Value;
            var right = (Reducer.Reduce(casted.Right, ctx) as Constant).Value;
            switch (casted.OperatorType)
            {
                case BinaryOperatorType.Divide:
                    var prevCtx = ctx.GetMathContext();
                    var context = new MathContext(prevCtx.Precision != 0 ? prevCtx.Precision : 1000, prevCtx.RoundingMode == RoundingMode.Unnecessary ? RoundingMode.HalfEven : prevCtx.RoundingMode);
                    return new Constant(BigMath.Divide(left, right, context));
                case BinaryOperatorType.Minus:
                    return new Constant(BigMath.Subtract(left, right, ctx.GetMathContext()));
                case BinaryOperatorType.Multiply:
                    return new Constant(BigMath.Multiply(left, right, ctx.GetMathContext()));
                case BinaryOperatorType.Plus:
                    return new Constant(BigMath.Add(left, right, ctx.GetMathContext()));
                case BinaryOperatorType.Pow:
                    return new Constant(BigMath.Pow(left, right.ToInt32(), ctx.GetMathContext()));
                case BinaryOperatorType.Prm:
                    return new BinaryExpression(
                        new UnaryExpression(UnaryOperatorType.Factorial, casted.Left),
                        BinaryOperatorType.Divide, 
                        new UnaryExpression(UnaryOperatorType.Factorial, new BinaryExpression(casted.Left, BinaryOperatorType.Minus, casted.Right))
                        );
                case BinaryOperatorType.Pwd:
                    var t = right.ToInt32();
                    BinaryExpression final = null;
                    for(var i = t; i >= 1; i--)
                    {
                        var add = new BinaryExpression(casted.Left, BinaryOperatorType.Pow, new Constant(new BigDecimal(i)));
                        if (final == null)
                        {
                            final = new BinaryExpression(new Constant(BigDecimal.Zero), BinaryOperatorType.Plus, add);
                        } else
                        {
                            final = new BinaryExpression(final, BinaryOperatorType.Plus, add);
                        }
                    }
                    return final;
            }
            return null;
        }
    }
}
