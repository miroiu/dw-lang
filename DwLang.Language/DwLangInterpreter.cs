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
                .ToDictionary(x => (x.GetCustomAttributes(typeof(ExpressionEvaluatorAttribute), true).First() as ExpressionEvaluatorAttribute).ExpressionType, x => Activator.CreateInstance(x) as IExpressionEvaluator);

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
}
