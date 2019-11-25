using DwLang.Language.Expressions;
using DwLang.Language.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DwLang.Language
{
    public class DwLangInterpreter
    {
        private readonly IOutputStream _output;

        public static readonly IDictionary<Type, IExpressionEvaluator> Evaluators = typeof(DwLangInterpreter).Assembly.GetTypes()
                .Where(x => typeof(IExpressionEvaluator).IsAssignableFrom(x) && x.CustomAttributes.Any())
                .SelectMany(x => {
                    return x.GetCustomAttributes(false).Select(y => new AttrWithType { 
                        Attr = y as ExpressionEvaluatorAttribute,
                        Type = x
                    }).ToList();
                })
                .ToDictionary(x => (x.Attr as ExpressionEvaluatorAttribute).ExpressionType, x => Activator.CreateInstance(x.Type) as IExpressionEvaluator);

        public DwLangInterpreter(IOutputStream output)
        {
            _output = output;
        }

        public async Task Run(Expressions.Expression root)
        {
            await Task.CompletedTask;
            using (var ctx = new ExecutionContext(_output))
            {
                var evaluator = Evaluators[root.GetType()];
                var _ = evaluator.Evaluate(root, ctx);
            }
        }
    }

    internal class AttrWithType
    {
        public Type Type { get; set; }
        public ExpressionEvaluatorAttribute Attr { get; set; }
    }
}
