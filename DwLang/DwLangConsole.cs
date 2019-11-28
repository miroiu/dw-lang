using DwLang.Language;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DwLang
{
    public class DwLangConsole : IOutputStream
    {
        private class CommandComparer : IEqualityComparer<(string Name, string)>
        {
            public bool Equals((string Name, string) x, (string Name, string) y)
                => x.Name == y.Name;

            public int GetHashCode((string Name, string) obj)
                => obj.Name.GetHashCode();
        }

        public static readonly Dictionary<(string Name, string Description), IConsoleCommand> Commands = typeof(IConsoleCommand).Assembly.GetTypes()
            .Where(t => typeof(IConsoleCommand).IsAssignableFrom(t) && t.CustomAttributes.Any())
            .SelectMany(t =>
                t.GetCustomAttributes(false).SelectMany(y =>
                {
                    var attr = (ConsoleCommandAttribute)y;
                    return attr.Aliases.Select(a => new
                    {
                        Name = a,
                        attr.Description,
                        Type = t
                    }).ToList();
                }))
            .ToDictionary(x => (x.Name, x.Description), x => (IConsoleCommand)Activator.CreateInstance(x.Type), new CommandComparer());

        private readonly string[] _arguments;

        public DwLangConsole(string[] arguments)
            => _arguments = arguments;

        public void WriteLine(string line)
            => Console.WriteLine(line);

        public string[] GetArguments()
            => _arguments;

        public void Clear()
            => Console.Clear();

        public string ReadLine()
        {
            Console.Write("> ");
            return Console.ReadLine();
        }

        public void Write(string value)
            => Console.Write(value);

        public void RunCommand(string cmd)
        {
            if (Commands.TryGetValue((cmd, default), out var command))
            {
                command.Execute(this);
            }
            else
            {
                RunCommand("-help");
            }
        }
    }
}
