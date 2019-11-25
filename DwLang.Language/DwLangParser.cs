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

        public DwLangParser(DwLangLexer lexer)
        {
            _lexer = lexer;
            HasNext = _lexer.Peek().Type != TokenType.EndOfCode;
        }

        public Expression Next()
        {
            var token = _lexer.Lex();
            if (token.Type == TokenType.EndOfCode)
            {
                HasNext = false;
                return default;
            }

            if (Parslets.TryGetValue((token.Type, true), out var parslet))
            {
                var result = parslet.Accept(this, token);

                Match(TokenType.Semicolon);

                HasNext = _lexer.Peek().Type != TokenType.EndOfCode;

                return result;
            }

            throw new DwLangParserException($"Unexpected token {token.Type} in primary epression.");
        }

        public Expression ParseExpression(OperatorPrecedence operatorPrecedence = OperatorPrecedence.None)
        {
            return ParseBinaryExpression(parentPrecedence: operatorPrecedence);
        }

        private Expression ParseBinaryExpression(Expression left = default, OperatorPrecedence parentPrecedence = OperatorPrecedence.None)
        {
            var token = Take();

            if (left == default)
            {
                if (token.Type.IsOperator() && Parslets.TryGetValue((token.Type, false), out var parslet))
                {
                    if (parslet is UnaryParslet unary)
                    {
                        left = unary.Accept(this, token);
                    }
                    else
                    {
                        left = ParsePrimaryExpression();
                    }
                }
                else
                {
                    left = ParsePrimaryExpression();
                }
            }

            while (token.Type != TokenType.Semicolon)
            {
                if (token.Type.IsOperator() && Parslets.TryGetValue((token.Type, false), out var parslet))
                {
                    var precendence = token.Type.ToOperatorPrecedence();

                    if (parentPrecedence >= precendence)
                    {
                        return left;
                    }

                    left = new BinaryExpression(left, token.Type.ToBinaryOperatorType(), ParseBinaryExpression(parentPrecedence: precendence));
                }

                return left;
            }

            return left;
        }

        private Expression ParsePrimaryExpression()
        {
            var token = _lexer.Lex();
            if (Parslets.TryGetValue((token.Type, false), out var parslet))
            {
                return parslet.Accept(this, token);
            }

            return ParseBinaryExpression();
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

        public Token Peek(int offset = 1)
        {
            return _lexer.Peek(offset);
        }

        public Token Take()
        {
            return _lexer.Lex();
        }
    }
}
