using Deveel.Math;
using DwLang.Language.Expressions;

namespace DwLang.Language.Interpreter
{
    [ExpressionEvaluator(typeof(UnaryExpression))]
    public class UnaryEvaluator : IExpressionEvaluator
    {
        public Expression Evaluate(Expression expression, ExecutionContext ctx)
        {
            var casted = expression as UnaryExpression;
            var value = (DwLangInterpreter.Evaluators[casted.Operand.GetType()].Evaluate(casted.Operand, ctx) as Constant).Value;
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

        public static BigDecimal Factorial(BigDecimal n, MathContext ctx)
        {
            var factorial = BigDecimal.One;
            for (var i = 1; i <= n.ToInt32(); i++)
            {
                factorial = BigMath.Multiply(factorial, new BigDecimal(i), ctx);
            }
            return factorial;
        }

        public static BigInteger Sqrt(BigInteger n, MathContext ctx)
        {
            BigInteger a = BigInteger.One;
            BigInteger b = BigMath.Add(BigMath.ShiftRight(a, 5), BigInteger.Parse("8"), ctx);

            while (b.CompareTo(a) >= 0)
            {
                var mid = BigMath.ShiftRight(BigMath.Add(a, b, ctx), 1);

                if (BigMath.Multiply(mid, mid, ctx).CompareTo(n) > 0)
                {
                    b = BigMath.Subtract(mid, BigInteger.One, ctx);
                } else
                {
                    a = BigMath.Add(mid, BigInteger.One, ctx);
                }
            }
            return BigMath.Subtract(a, BigInteger.One, ctx);
        }
    }
}
