using System;

namespace DwLang.Language.Parser
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ParsletAttribute : Attribute
    {
        public ParsletAttribute(TokenType type)
        {
            TokenType = type;
        }

        public TokenType TokenType { get; }
    }
}