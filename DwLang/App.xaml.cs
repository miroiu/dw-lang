using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DwLang
{
    public partial class App : Application
    {
        private static readonly Dictionary<string, IConsoleCommand> _commands = typeof(IConsoleCommand).Assembly.GetTypes()
            .Where(t => typeof(IConsoleCommand).IsAssignableFrom(t) && t.CustomAttributes.Any())
            .SelectMany(t =>
            {
                return t.GetCustomAttributes(false).Select(y => new
                {
                    Attribute = (ConsoleCommandAttribute)y,
                    Type = t
                }).ToList();
            }).ToDictionary(x => x.Attribute.Name, x => (IConsoleCommand)Activator.CreateInstance(x.Type));

        private void OnStartup(object sender, StartupEventArgs e)
        {
            using var console = new DwLangReplConsole(e.Args.Skip(1).ToArray());

            if (e.Args.Length == 0)
            {
                MainWindow = new MainWindow();
                MainWindow.Show();
                return;
            }
            else if (_commands.TryGetValue(e.Args[0], out var command))
            {
                console.Show();
                command.Execute(console);
            }
            else
            {
                console.Show();
                _commands["-help"].Execute(console);
            }

            Shutdown();
        }
    }
}
