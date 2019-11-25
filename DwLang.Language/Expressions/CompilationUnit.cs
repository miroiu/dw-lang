using System.Collections.Generic;

namespace DwLang.Language.Expressions
{
    public class CompilationUnit : Expression
    {
        public CompilationUnit(Expression[] statements)
        {
            Statements = statements;
        }

        public IReadOnlyCollection<Expression> Statements { get; }
    }
}
