using Deveel.Math;
using DwLang.Language.Expressions;
using System;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(BinaryExpression))]
    public class BinaryExpressionEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            try
            {
                var casted = expression as BinaryExpression;
                var left = (Reducer.Reduce(casted.Left, ctx) as Constant).Value;
                var right = (Reducer.Reduce(casted.Right, ctx) as Constant).Value;

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
                        return new Constant(BigMath.StripTrailingZeros(result));

                    case BinaryOperatorType.Minus:
                        return new Constant(BigMath.StripTrailingZeros(BigMath.Subtract(left, right, ctx.MathContext)));

                    case BinaryOperatorType.Multiply:
                        return new Constant(BigMath.StripTrailingZeros(BigMath.Multiply(left, right, ctx.MathContext)));

                    case BinaryOperatorType.Plus:
                        return new Constant(BigMath.StripTrailingZeros(BigMath.Add(left, right, ctx.MathContext)));

                    case BinaryOperatorType.Pow:
                        return new Constant(BigMath.StripTrailingZeros(BigMath.Pow(left, right.ToInt32(), ctx.MathContext)));

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
                            var add = new BinaryExpression(casted.Left, BinaryOperatorType.Pow, new Constant(new BigDecimal(i)));
                            if (final == null)
                            {
                                final = new BinaryExpression(new Constant(BigDecimal.Zero), BinaryOperatorType.Plus, add);
                            }
                            else
                            {
                                final = new BinaryExpression(final, BinaryOperatorType.Plus, add);
                            }
                        }
                        return final;
                }

                throw new DwLangExecutionException($"Operator type {casted.OperatorType} is not defined.", casted);
            }
            catch (Exception e)
            {
                if (e is DwLangExecutionException)
                {
                    throw e;
                }
                throw new DwLangExecutionException(e, expression);
            }
        }
    }
}
