using Deveel.Math;
using System;
using System.Collections.Generic;

namespace DwLang.Language.Interpreter
{
    public class ExecutionContext : IDisposable
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
            if (!_values.ContainsKey(name)) {
                throw new Exception("Variable is not declared");
            }
            _values[name] = value;
        }

        public void Declare(string name, BigDecimal value)
        {
            if (_values.ContainsKey(name)) {
                throw new Exception("Variable is already declared");
            }
            _values[name] = value;
        }

        public void Print(BigDecimal value)
        {
            var v = new BigDecimal(value.UnscaledValue, value.Scale, MathContext);
            var str = v.ToPlainString().Replace('.', ',');
            _out.WriteLine(str);
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

        public MathContext GetMathContext()
        {
            return MathContext;
        }

        public BigDecimal Get(string name)
        {
            return _values[name];
        }

        public void Dispose()
        {
            
        }
    }
}
