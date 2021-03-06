﻿using Deveel.Math;
using DwLang.Language.Expressions;
using System;
using System.Collections.Generic;

namespace DwLang.Language.Interpreter
{
    public class ExecutionContext
    {
        private readonly IDictionary<string, BigDecimal> _values = new Dictionary<string, BigDecimal>();
        public MathContext MathContext { get; private set; } = new MathContext(0, RoundingMode.Unnecessary);
        private readonly IOutputStream _out;

        public ExecutionContext(IOutputStream outs)
        {
            _out = outs;
        }

        public void Assign(string name, BigDecimal value)
        {
            if (!_values.ContainsKey(name))
            {
                throw new Exception("Variable is not declared");
            }
            _values[name] = value;
        }

        public void Declare(string name, BigDecimal value)
        {
            if (_values.ContainsKey(name))
            {
                throw new Exception("Variable is already declared");
            }
            _values[name] = value;
        }

        public void Print(BigDecimal value)
        {
            var v = new BigDecimal(value.UnscaledValue, value.Scale, MathContext);
            _out.WriteLine(v.ToPlainString(/*new NumberFormatInfo
            {
                NumberDecimalSeparator = ",",
            })*/).Replace('.', ','));
        }

        public void Clear()
        {
            _out.Clear();
        }

        public void SetCurrentPrecision(int precision)
        {
            if (precision == 0)
            {
                MathContext = new MathContext(0, RoundingMode.Unnecessary);
            }
            else
            {
                MathContext = new MathContext(precision, RoundingMode.HalfUp);
            }
        }

        public BigDecimal Get(string name, Expression expr)
        {
            if (!_values.ContainsKey(name))
            {
                throw new DwLangExecutionException($"Variable {name} is not defined.", expr);
            }
            return _values[name];
        }
    }
}
