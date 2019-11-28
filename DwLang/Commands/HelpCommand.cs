namespace DwLang
{
    [ConsoleCommand("-h")]
    [ConsoleCommand("-help")]
    public class HelpCommand : IConsoleCommand
    {
        public void Execute(DwLangReplConsole console)
        {
            console.WriteLine($"-r\t> read-eval-print-loop");
            console.WriteLine($"-repl\t> read-eval-print-loop");
            console.WriteLine($"-f\t> execute file");
            console.WriteLine($"-file\t> execute file");
        }
    }
}
