using DwLang.Language;
using DwLang.Language.Interpreter;
using DwLang.Language.Lexer;
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
        [TestCase("var a; a = 4; print a;", new string[] { "4" })]
        [TestCase("var a = 2;;;;;;;; print a;", new string[] { "2" })]
        [TestCase(";;;;;;;;;", new string[0])]
        [TestCase("var _ = 2; print _;", new string[] { "2" })]
        [TestCase("var s_ = 2; print s_;", new string[] { "2" })]
        [TestCase("var _s = 2; print _s;", new string[] { "2" })]
        [TestCase("var _s_ = 2; print _s_;", new string[] { "2" })]
        [TestCase("var s_s = 2; print s_s;", new string[] { "2" })]
        [TestCase("set precision 4; print 7 : 6;", new string[] { "1,1667" })]
        [TestCase("print 5 + 3;", new string[] { "8" })]
        [TestCase("print 5 - 3;", new string[] { "2" })]
        [TestCase("print 5 : 2;", new string[] { "2,5" })]
        [TestCase("print 3 : 3;", new string[] { "1" })]
        [TestCase("print 5 x 2;", new string[] { "10" })]
        [TestCase("print 2pow3;", new string[] { "8" })]
        [TestCase("print 2 pow3;", new string[] { "8" })]
        [TestCase("print 2pow 3;", new string[] { "8" })]
        [TestCase("print 2 pow 3;", new string[] { "8" })]
        [TestCase("print 3 prm 3;", new string[] { "6" })]
        [TestCase("print 52 pwd 8;", new string[] { "54507958502660" })]
        [TestCase("print sqr 81;", new string[] { "9" })]
        [TestCase("print (3 + sqr 9);", new string[] { "6" })]
        [TestCase("print 4!;", new string[] { "24" })]
        [TestCase("var a = 5; print 10:a;", new string[] { "2" })]
        [TestCase("print avg 2 3 7;", new string[] { "4" })]
        [TestCase("print med 2 4 7 8 9;", new string[] { "7" })]
        [TestCase("print med 8 3 5 9;", new string[] { "6,5" })]
        [TestCase("print (med 8 3 5 9) + 0,5;", new string[] { "7" })]
        [TestCase("/* some comment *\\ print 5; *\\ print 3;", new string[] { "3" })]
        [TestCase("/\r\n*\r\n some comment \r\n*\r\n\\ print 5; *\\ print 3;", new string[] { "3" })]
        [TestCase("print 1;/\r\n*\r\n some comment \r\n*\r\n\\ print 5; *\\ print 3;", new string[] { "1", "3" })]
        [TestCase("/* var val = 2; print val;/* print val; *\\ print 4;", new string[] { "4" })]
        [TestCase("/* var val = 2; print val;/* print \r\n val; *\\ print 4;", new string[] { "4" })]
        [TestCase("set precision 4; print 5 : 2;", new string[] { "2,5" })]
        [TestCase("print 5,5 - 3,5;", new string[] { "2" })]
        [TestCase("print 0,5 x 2;", new string[] { "1" })]
        [TestCase("print 30 - 10;", new string[] { "20" })]
        [TestCase("print 30 x 2;", new string[] { "60" })]
        [TestCase("print 30 : 3;", new string[] { "10" })]
        public void Everything(string input, string[] expected)
        {
            var result = App.Run(input);
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase("var x;", typeof(DwLangParserException))]
        [TestCase("var pwd;", typeof(DwLangParserException))]
        [TestCase("var var;", typeof(DwLangParserException))]
        [TestCase("var sqr;", typeof(DwLangParserException))]
        [TestCase("var pow;", typeof(DwLangParserException))]
        [TestCase("var prm;", typeof(DwLangParserException))]
        [TestCase("var avg;", typeof(DwLangParserException))]
        [TestCase("var med;", typeof(DwLangParserException))]
        [TestCase("var set;", typeof(DwLangParserException))]
        [TestCase("print sqr(print 3);", typeof(DwLangParserException))]
        [TestCase("var precision;", typeof(DwLangParserException))]
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
