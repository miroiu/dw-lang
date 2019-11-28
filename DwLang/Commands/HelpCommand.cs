namespace DwLang
{
    [ConsoleCommand("-h", "-?", "-help", Description = "Shows a list of commands")]
    public class HelpCommand : IConsoleCommand
    {
        public void Execute(DwLangConsole console)
        {
            var commands = DwLangConsole.Commands;

            foreach (var kvp in commands)
            {
                console.WriteLine($"{kvp.Key.Name}\t{kvp.Key.Description}");
            }
        }
    }
}
