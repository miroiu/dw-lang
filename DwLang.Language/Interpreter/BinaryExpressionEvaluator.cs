﻿using Deveel.Math;
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
                        var tempContext = new MathContext(ctx.GetMathContext().Precision + 1, ctx.GetMathContext().RoundingMode);
                        var result = BigMath.Divide(left, right, tempContext);
                        return new Constant(new BigDecimal(result, result.Scale, ctx.GetMathContext()));
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
                return null;
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
