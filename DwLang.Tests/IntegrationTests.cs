using DwLang.Language;
using DwLang.Language.Interpreter;
using DwLang.Language.Parser;
using NUnit.Framework;
using System;

namespace DwLang.Tests
{
    public static class App
    {
        public static string[] Run(string code)
        {
            var mockOutput = new Mocks.MockOutputStream();
            var preLexer = new DwLangPreLexer(code);
            var source = preLexer.Sanitize();
            var lexer = new DwLangLexer(source);
            var parser = new DwLangParser(lexer);

            var interpreter = new DwLangInterpreter(mockOutput);
            interpreter.Run(parser);
            return mockOutput.CurrentOutput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        }
    }

    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        [TestCase("var a = 2; print a;", new string[] { "2" })]
        [TestCase("var a; a = 2,5; print a;", new string[] { "2,5" })]
        [TestCase("set precision 4; print 7 : 6;", new string[] { "1,1667" })]
        public void Everything(string input, string[] expected)
        {
            var result = App.Run(input);
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase("var x;", typeof(DwLangParserException))]
        [TestCase("var pwd;", typeof(DwLangParserException))]
        [TestCase("print a;", typeof(DwLangExecutionException))]
        public void Exceptions(string input, Type exType)
        {
            try
            {
                var result = App.Run(input);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exType, ex.GetType());
            }
        }
    }
}
