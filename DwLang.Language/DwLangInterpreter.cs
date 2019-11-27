﻿using DwLang.Language.Expressions;
using DwLang.Language.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DwLang.Language
{
    public class DwLangInterpreter
    {
        private readonly IOutputStream _output;

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
        }

        public void Run(IExpressionProvider provider)
        {
            using (var ctx = new ExecutionContext(_output))
            {
                while (provider.HasNext)
                {
                    var expr = provider.Next();
                    if (!(expr is EmptyExpression))
                    {
                        Reducer.Reduce(expr, ctx);
                    }
                }
            }
        }
    }
}
