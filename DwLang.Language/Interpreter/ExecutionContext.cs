using Deveel.Math;
using System;
using System.Collections.Generic;

namespace DwLang.Language.Interpreter
{
    public class ExecutionContext : IDisposable
    {
        private readonly IDictionary<string, BigDecimal> _values = new Dictionary<string, BigDecimal>();
        public static MathContext MathContext { get; private set; } = new MathContext(0, RoundingMode.Unnecessary);

        public void Assign(string name, BigDecimal value)
        {
            if (!_values.ContainsKey(name))
            {
                throw new DwLangExecutionException($"Variable {name} is not initialized.");
            }
            _values[name] = value;
        }

        public void Declare(string name, BigDecimal value)
        {
            if (_values.ContainsKey(name))
            {
                throw new DwLangExecutionException($"Variable {name} is already initialized.");
            }
            _values[name] = value;
        }

        public void SetCurrentPrecision(int precision)
        {
            MathContext = new MathContext(precision, RoundingMode.HalfUp);
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
