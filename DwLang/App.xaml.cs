using DwLang.Language;
using System.IO;
using System.Windows;

namespace DwLang
{
    public partial class App : Application
    {
        public DwLangReplConsole Console = new DwLangReplConsole();

        private void OnStartup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length == 0)
            {
                MainWindow = new MainWindow();
                MainWindow.Show();
            }
            else if (e.Args.Length > 1)
            {
                Console.WriteLine("Only one file at a time is allowed.");
            }
            else
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), e.Args[0]);

                if (e.Args[0] == "repl")
                {
                    string code;
                    do
                    {
                        Console.WriteLine("> ");
                        code = Console.ReadLine();

                        RunCode(code, true);
                    }
                    while (code != "exit");
                }
                else if (File.Exists(path))
                {
                    var source = File.ReadAllText(path);

                    RunCode(source, false);
                    Shutdown();
                }
                else
                {
                    System.Console.WriteLine($"File {e.Args[0]} does not exist.");
                    Shutdown();
                }
            }
        }

        private void RunCode(string source, bool repl)
        {
            var preLexer = new DwLangPreLexer(source);
            var stream = preLexer.Sanitize();
            var lexer = new DwLangLexer(stream);
            var parser = new DwLangParser(lexer);

            var interpreter = new DwLangInterpreter(Console);
            interpreter.Run(parser);
        }
    }
}
