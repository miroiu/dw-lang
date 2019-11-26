using Deveel.Math;
using DwLang.Language.Expressions;
using System;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(UnaryExpression))]
    public class UnaryEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            try
            {
                var casted = expression as UnaryExpression;
                var value = (Reducer.Reduce(casted.Operand, ctx) as Constant).Value;
                switch (casted.OperatorType)
                {
                    case UnaryOperatorType.Factorial:
                        return new Constant(Factorial(value, ctx.GetMathContext()));

                    case UnaryOperatorType.Sqr:
                        return new Constant(Sqrt(value.ToBigInteger(), ctx.GetMathContext()));

                    case UnaryOperatorType.Print:
                        ctx.Print(value);
                        return null;
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

        public static BigDecimal Factorial(BigDecimal n, MathContext ctx)
        {
            var factorial = BigDecimal.One;
            for (var i = 1; i <= n.ToInt32(); i++)
            {
                factorial = BigMath.Multiply(factorial, new BigDecimal(i), ctx);
            }
            return factorial;
        }

        public static BigDecimal Sqrt(BigInteger n, MathContext ctx)
        {
            return new BigDecimal(Math.Sqrt(n.ToDouble()), ctx);
        }
    }
}
