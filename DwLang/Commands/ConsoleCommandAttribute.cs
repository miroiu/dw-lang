using System;
using System.Collections.Generic;

namespace DwLang
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ConsoleCommandAttribute : Attribute
    {
        public ConsoleCommandAttribute(params string[] name)
        {
            Aliases = name;
        }

        public IReadOnlyCollection<string> Aliases { get; }
        public string Description { get; set; }
    }
}
