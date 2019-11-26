using DwLang.Language;
using System;
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

            try
            {
                var interpreter = new DwLangInterpreter(Console);
                await interpreter.Run(parser);
            }
            catch (DwLangLexerException lexEx)
            {
                Console.WriteLine($"[{lexEx.Line}, {lexEx.Column}]: {lexEx.Message}");
            }
            catch (DwLangException dwLangEx)
            {
                Console.WriteLine(dwLangEx.ToString());
            }
            catch (ArithmeticException arEx)
            {
                Console.WriteLine(arEx.Message);
            }
            catch
            {
                Console.WriteLine("Catastrophic failure!");
            }
        }
    }
}
