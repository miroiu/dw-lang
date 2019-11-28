using System.IO;

namespace DwLang
{
    [ConsoleCommand("-f")]
    [ConsoleCommand("-file")]
    public class FileCommand : IConsoleCommand
    {
        private DwLangRepl _repl;
        private const string _extension = ".dwl";

        public void Execute(DwLangReplConsole console)
        {
            _repl = new DwLangRepl(console);

            var arguments = console.GetArguments();
            var path = Path.Combine(Directory.GetCurrentDirectory(), arguments[0]);

            if (Path.GetExtension(arguments[0]) == _extension && File.Exists(path))
            {
                var source = File.ReadAllText(path);
                _repl.Evaluate(source);
            }
        }
    }
}
