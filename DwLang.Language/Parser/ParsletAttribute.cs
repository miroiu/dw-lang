using DwLang.Language.Lexer;
using System;

namespace DwLang.Language.Parser
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ParsletAttribute : Attribute
    {
        public ParsletAttribute(TokenType type, bool canBeginWith = false)
        {
            TokenType = type;
            CanBeginWith = canBeginWith;
        }

        public TokenType TokenType { get; }
        public bool CanBeginWith { get; }
    }
}