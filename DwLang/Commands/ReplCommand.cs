namespace DwLang
{
    [ConsoleCommand("-r")]
    [ConsoleCommand("-repl")]
    public class ReplCommand : IConsoleCommand
    {
        private DwLangRepl _repl;

        public void Execute(DwLangReplConsole console)
        {
            _repl = new DwLangRepl(console);

            while (true)
            {
                var line = console.ReadLine();
                _repl.Evaluate(line);
            }
        }
    }
}
