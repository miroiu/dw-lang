using System;

namespace DwLang.Language
{
    public abstract class DwLangException : Exception
    {
        public DwLangException(string message) : base(message)
        {
        }
    }
}
