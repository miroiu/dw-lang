using System;

namespace DwLang
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ConsoleCommandAttribute : Attribute
    {
        public ConsoleCommandAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
