using Deveel.Math;
using DwLang.Language.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

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
                    return new Constant(Factorial(value, 2)); // TODO replace 2
            }
            return null;
        }

        private BigDecimal Factorial(BigDecimal value, BigDecimal n)
        {
            if (n.Equals(BigDecimal.One))
            {
                return value;
            }
            BigDecimal lessOne = BigMath.Subtract(n, BigDecimal.One);
            return Factorial(BigMath.Multiply(value, lessOne), lessOne);
        }
    }
}
