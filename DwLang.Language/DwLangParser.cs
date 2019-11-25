using DwLang.Language.Expressions;
using DwLang.Language.Parser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DwLang.Language
{
    public class DwLangParser
    {
        public static readonly IDictionary<TokenType, IParslet> Parslets = typeof(DwLangInterpreter).Assembly.GetTypes()
                 .Where(x => typeof(IParslet).IsAssignableFrom(x) && x.CustomAttributes.Any())
                 .SelectMany(x =>
                 {
                     return x.GetCustomAttributes(false).Select(y => new
                     {
                         Attribute = (ParsletAttribute)y,
                         Type = x
                     }).ToList();
                 })
                 .ToDictionary(x => x.Attribute.TokenType, x => Activator.CreateInstance(x.Type) as IParslet);

        private readonly DwLangLexer _lexer;

        public DwLangParser(DwLangLexer lexer)
        {
            _lexer = lexer;
        }

        public Token Current { get; private set; }

        public Expression Parse()
        {
            var token = _lexer.Lex();
            var parslet = Parslets[token.Type];
            var result = parslet.Accept(this, token);

            Match(TokenType.Semicolon);

            return result;
        }

        public Token Match(TokenType tokenType)
        {
            var token = _lexer.Lex();

            if (token.Type == tokenType)
            {
                return token;
            }

            throw new DwLangParserException($"Expected {tokenType} but found {token.Type}");
        }

        //public Token Peek()
        //{
        //    if(_lexer.Pee)
        //}
    }
}
