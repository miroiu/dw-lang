﻿using DwLang.Language.Expressions;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.OpenParen)]
    public class GroupingParslet : IParslet
    {
        public Expression Accept(DwLangParser parser, Token token)
        {
            var expr = parser.ParsePrimaryExpression();
            //var closeParen = parser.Match(TokenType.CloseParen);
            return new Grouping(expr);
        }
    }
}