using System.Collections.Generic;

namespace DwLang.Language.Expressions
{
    public class VarArgsExpression : Expression
    {
        public VarArgsExpression(VarArgsOperatorType operatorType, Expression[] args)
        {
            OperatorType = operatorType;
            Arguments = args;
        }

        public VarArgsOperatorType OperatorType { get; }
        public IReadOnlyCollection<Expression> Arguments { get; }
    }
}
