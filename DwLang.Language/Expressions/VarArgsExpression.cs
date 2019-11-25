using System.Collections.Generic;

namespace DwLang.Language.Expressions
{
    public class VarArgsExpression : Expression
    {
        public VarArgsExpression(VarArgsOperatorType operatorType, Expression[] expressions)
        {
            OperatorType = operatorType;
            Expressions = expressions;
        }

        public VarArgsOperatorType OperatorType { get; }
        public IReadOnlyCollection<Expression> Expressions { get; }
    }
}
