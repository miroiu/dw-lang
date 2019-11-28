using DwLang.Language;
using DwLang.Language.Interpreter;
using DwLang.Language.Lexer;
using DwLang.Language.Parser;

namespace DwLang
{
    public class DwLangRepl
    {
        public DwLangInterpreter Interpreter;
        public DwLangConsole Console;

        public DwLangRepl(DwLangConsole console)
        {
            Console = console;
            Interpreter = new DwLangInterpreter(console);
        }

        public void Evaluate(string code)
        {
            try
            {
                var preLexer = new DwLangPreLexer(code);
                var stream = preLexer.Sanitize();
                var lexer = new DwLangLexer(stream);
                var parser = new DwLangParser(lexer);

                Interpreter.Run(parser);
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
