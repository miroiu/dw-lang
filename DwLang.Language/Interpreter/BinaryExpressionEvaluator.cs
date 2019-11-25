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
            var left = (DwLangInterpreter.Evaluators[casted.Left.GetType()].Evaluate(casted.Left, ctx) as Constant).Value;
            var right = (DwLangInterpreter.Evaluators[casted.Right.GetType()].Evaluate(casted.Right, ctx) as Constant).Value;
            switch (casted.OperatorType)
            {
                case BinaryOperatorType.Divide:
                    return new Constant(BigMath.Divide(left, right, ctx.GetMathContext()));
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
                    return null;
            }
            return null;
        }
    }
}
