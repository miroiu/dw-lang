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
            var left = (Reducer.Reduce(casted.Left, ctx) as ConstantExpression).Value;
            var right = (Reducer.Reduce(casted.Right, ctx) as ConstantExpression).Value;

            switch (casted.OperatorType)
            {
                case BinaryOperatorType.Divide:
                    BigDecimal result;
                    var mathCtx = ctx.MathContext;
                    if (mathCtx.Precision != 0)
                    {
                        result = BigMath.Divide(left, right, mathCtx.Precision, mathCtx.RoundingMode);
                    }
                    else
                    {
                        result = BigMath.Divide(left, right, mathCtx);
                    }
                    return new ConstantExpression(BigMath.StripTrailingZeros(result));

                case BinaryOperatorType.Minus:
                    return new ConstantExpression(BigMath.StripTrailingZeros(BigMath.Subtract(left, right, ctx.MathContext)));

                case BinaryOperatorType.Multiply:
                    return new ConstantExpression(BigMath.StripTrailingZeros(BigMath.Multiply(left, right, ctx.MathContext)));

                case BinaryOperatorType.Plus:
                    return new ConstantExpression(BigMath.StripTrailingZeros(BigMath.Add(left, right, ctx.MathContext)));

                case BinaryOperatorType.Pow:
                    return new ConstantExpression(BigMath.StripTrailingZeros(BigMath.Pow(left, right.ToInt32(), ctx.MathContext)));

                case BinaryOperatorType.Prm:
                    return new BinaryExpression(
                        new UnaryExpression(UnaryOperatorType.Factorial, casted.Left),
                        BinaryOperatorType.Divide,
                        new UnaryExpression(UnaryOperatorType.Factorial, new BinaryExpression(casted.Left, BinaryOperatorType.Minus, casted.Right))
                        );

                case BinaryOperatorType.Pwd:
                    var t = right.ToInt32();
                    BinaryExpression final = null;
                    for (var i = t; i >= 1; i--)
                    {
                        var add = new BinaryExpression(casted.Left, BinaryOperatorType.Pow, new ConstantExpression(new BigDecimal(i)));
                        if (final == null)
                        {
                            final = new BinaryExpression(new ConstantExpression(BigDecimal.Zero), BinaryOperatorType.Plus, add);
                        }
                        else
                        {
                            final = new BinaryExpression(final, BinaryOperatorType.Plus, add);
                        }
                    }
                    return final;
            }

            return default;
        }
    }
}
