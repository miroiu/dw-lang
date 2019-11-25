using Deveel.Math;
using DwLang.Language.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

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
                    return new Constant(left / right);
                case BinaryOperatorType.Minus:
                    return new Constant(left - right);
                case BinaryOperatorType.Multiply:
                    return new Constant(left * right);
                case BinaryOperatorType.Plus:
                    return new Constant(left + right);
                case BinaryOperatorType.Pow:
                    return new Constant(new BigDecimal(Math.Pow(left.ToInt64(), right.ToInt64()), ctx.GetMathContext()));
                case BinaryOperatorType.Prm:
                    return null; // TODO reuse expressions
            }
            return null;
        }
    }
}
