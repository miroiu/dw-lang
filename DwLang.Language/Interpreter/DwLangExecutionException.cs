using DwLang.Language.Expressions;
using System;

namespace DwLang.Language.Interpreter
{
    public class DwLangExecutionException : DwLangException
    {
        public Expression Expression { get; private set; }
        public DwLangExecutionException(string message, Expression e) : base(message)
        {
            Expression = e;
        }

        public DwLangExecutionException(Exception ex, Expression e) : base(ex.Message)
        {
            Expression = e;
        }
    }
}
