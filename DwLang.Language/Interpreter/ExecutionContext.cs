using Deveel.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwLang.Language.Interpreter
{
    public class ExecutionContext : IDisposable
    {
        private readonly IDictionary<string, BigDecimal> _values = new Dictionary<string, BigDecimal>();



        public void Dispose()
        {
            
        }
    }
}
