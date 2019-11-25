using DwLang.Language;
using System.Windows.Input;

namespace DwLang
{
    public class DwLangApp : DwLangObservable
    {
        public DwLangApp()
        {
            RunCommand = new DwLangCommand(Run);
            Console = new DwLangConsole();
            Interpreter = new DwLangInterpreter(Console);
        }

        public DwLangConsole Console { get; }
        public DwLangInterpreter Interpreter { get; }
        public ICommand RunCommand { get; }

        private async void Run()
        {
            var code = Console.ReadLine();
            var source = new SourceText(code);

            var lexer = new DwLangLexer(source);
            while (true)
            {
                var currentToken = lexer.Lex();
                Console.WriteLine($"{currentToken.Text} - {currentToken.Type.ToString()}");
                if (currentToken.Type == TokenType.EndOfCode)
                {
                    break;
                }
            }
            var parser = new DwLangParser(lexer);
            var root = parser.Parse();

            try
            {
                await Interpreter.Run(root);
            }
            catch(DwLangException ex)
            {
                throw ex;
            }
        }
    }
}
