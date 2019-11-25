using System;
using System.Collections.Generic;
using System.Text;

namespace DwLang.Language.Interpreter
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class ExpressionEvaluatorAttribute : Attribute
    {
        public Type ExpressionType { get; private set; }

        public ExpressionEvaluatorAttribute(Type expressionType)
        {
            ExpressionType = expressionType;
        }
    }
}
