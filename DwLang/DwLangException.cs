using System;
using System.Collections.Generic;
using System.Text;

namespace DwLang
{
    public class DwLangException : Exception
    {
        public DwLangException(string message) : base(message)
        {
        }
    }
}
