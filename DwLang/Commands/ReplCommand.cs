namespace DwLang
{
    [ConsoleCommand("-r", "-repl", Description = "read-eval-print-loop")]
    public class ReplCommand : IConsoleCommand
    {
        private DwLangRepl _repl;

        public void Execute(DwLangConsole console)
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
