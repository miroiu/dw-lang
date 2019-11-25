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
                
            }
            return null;
        }
    }
}
