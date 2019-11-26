using DwLang.Language;
using DwLang.Language.Interpreter;
using DwLang.Language.Parser;
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
            catch (DwLangExecutionException dwLangExx)
            {
                Console.WriteLine($"[{dwLangExx.Expression.Token.Line}, {dwLangExx.Expression.Token.Column}]: {dwLangExx.Message}");
            }
            catch (DwLangParserException dwLangParserEx)
            {
                Console.WriteLine($"[{dwLangParserEx.Token.Line}, {dwLangParserEx.Token.Column}]: {dwLangParserEx.Message}");
            }
            catch (DwLangException dwLangEx)
            {
                Console.WriteLine(dwLangEx.ToString());
            }
            catch
            {
                Console.WriteLine("Catastrophic failure!");
            }
        }
    }
}
