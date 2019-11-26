using System.Diagnostics;

namespace DwLang.Language.Expressions
{
    [DebuggerDisplay("{GetType().Name}")]
    public abstract class Expression
    {
        public Token Token { get; set; }
    }
}