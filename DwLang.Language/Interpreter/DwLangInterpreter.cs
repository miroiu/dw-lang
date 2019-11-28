using DwLang.Language.Expressions;
using DwLang.Language.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DwLang.Language.Interpreter
{
    public class DwLangInterpreter
    {
        private readonly IOutputStream _output;
        public ExecutionContext Context { get; }

        public static readonly IDictionary<Type, IExpressionEvaluator> Evaluators = typeof(DwLangInterpreter).Assembly.GetTypes()
                .Where(x => typeof(IExpressionEvaluator).IsAssignableFrom(x) && x.CustomAttributes.Any())
                .SelectMany(x =>
                {
                    return x.GetCustomAttributes(false).Select(y => new
                    {
                        Attribute = (ExpressionEvaluatorAttribute)y,
                        Type = x
                    }).ToList();
                })
                .ToDictionary(x => x.Attribute.ExpressionType, x => Activator.CreateInstance(x.Type) as IExpressionEvaluator);

        public DwLangInterpreter(IOutputStream output)
        {
            _output = output;
            Context = new ExecutionContext(_output);
        }

        public void Run(IExpressionProvider provider)
        {
            while (provider.HasNext)
            {
                var expr = provider.Next();
                if (!(expr is EmptyExpression))
                {
                    Reducer.Reduce(expr, Context);
                }
            }
        }
    }
}
