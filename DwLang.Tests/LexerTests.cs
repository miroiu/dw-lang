using System;
using DwLang.Language;
using DwLang.Language.Interpreter;
using DwLang.Tests.Mocks;
using NUnit.Framework;

namespace DwLang.Tests
{
  public class LexerTests
  {
    private ExecutionContext _ctx;
    private MockOutputStream _out;

    [SetUp]
    public void Setup()
    {
      _out = new MockOutputStream();
      _ctx = new ExecutionContext(_out);
    }

    [Test]
    public void Comments_Should_Be_Ignored()
    {
      var input = "var a = 2;"+Environment.NewLine+"asd/* print a;";
      var preLexer = new DwLangPreLexer(input);
      var source = preLexer.Sanitize();
      var lexer = new DwLangLexer(source);
      var parser = new DwLangParser(lexer);
      var interpreter = new DwLangInterpreter(_out);
      interpreter.Run(parser).Wait();
      _out.CurrentOutput.ToString();
    }
  }
}