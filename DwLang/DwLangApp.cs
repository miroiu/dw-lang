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
        }

        public DwLangConsole Console { get; }
        public ICommand RunCommand { get; }

        private async void Run()
        {
            var code = Console.ReadLine();
            var source = new SourceText(code);

            var lexer = new DwLangLexer(source);
            var parser = new DwLangParser(lexer);

            var root = parser.Parse();

            PrintTokens(lexer);

            try
            {
                var interpreter = new DwLangInterpreter(Console);
                await interpreter.Run(root);
            }
            catch (DwLangException ex)
            {
                throw ex;
            }
        }

        private void PrintTokens(DwLangLexer lexer)
        {
            while (true)
            {
                var currentToken = lexer.Lex();
                Console.WriteLine($"{currentToken.Type.ToString()} - {currentToken.Text}");

                if (currentToken.Type == TokenType.EndOfCode)
                {
                    break;
                }
            }
        }
    }
}
