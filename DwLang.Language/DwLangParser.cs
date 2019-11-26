using DwLang.Language.Expressions;
using DwLang.Language.Parser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DwLang.Language
{
    public class DwLangParser : IExpressionProvider
    {
        public static readonly IDictionary<(TokenType Type, bool CanBeginWith), IParslet> Parslets = typeof(DwLangInterpreter).Assembly.GetTypes()
                 .Where(x => typeof(IParslet).IsAssignableFrom(x) && x.CustomAttributes.Any())
                 .SelectMany(x =>
                 {
                     return x.GetCustomAttributes(false).Select(y => new
                     {
                         Attribute = (ParsletAttribute)y,
                         Type = x
                     }).ToList();
                 })
                 .ToDictionary(x => (x.Attribute.TokenType, x.Attribute.CanBeginWith), x => Activator.CreateInstance(x.Type) as IParslet);

        private readonly DwLangLexer _lexer;

        public bool HasNext { get; private set; }
        public Token Current { get; private set; }

        public DwLangParser(DwLangLexer lexer)
        {
            _lexer = lexer;

            var peek = Peek();
            HasNext = peek.Type != TokenType.EndOfCode && peek.Type != TokenType.Semicolon;
        }

        public Expression Next()
        {
            Current = _lexer.Lex();
            var result = Parslets[(Current.Type, true)].Accept(this);

            Check(TokenType.Semicolon);

            //while(Current.Type == TokenType.Semicolon)
            //{
            //    Take();
            //}

            HasNext = Current.Type != TokenType.EndOfCode;

            return result;
        }

        public Expression ParseExpression()
            => Parslets[(TokenType.X, false)].Accept(this);

        public Expression ParsePrimaryExpression()
        {
            if (Parslets.TryGetValue((Current.Type, false), out var parslet))
            {
                return parslet.Accept(this);
            }

            throw new DwLangParserException(Current, $"Unexpected token {Current.Type}.");
        }

        public Token Take(TokenType tokenType)
        {
            if (Current.Type == tokenType)
            {
                return Take();
            }

            throw new DwLangParserException(Current, $"Expected {tokenType} but found {Current.Type}.");
        }

        public Token Peek(int offset = 1)
        {
            return _lexer.Peek(offset);
        }

        public Token Take()
        {
            var previous = Current;
            Current = _lexer.Lex();
            return previous;
        }

        public void Check(TokenType type)
        {
            if (Current.Type != type)
            {
                throw new DwLangParserException(Current, $"Expected {type} but found {Current.Type}.");
            }
        }

        public bool IsEndOfStatement()
            => Current.Type == TokenType.Semicolon;
    }
}
