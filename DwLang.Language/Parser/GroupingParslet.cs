﻿using DwLang.Language.Expressions;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.OpenParen)]
    public class GroupingParslet : IParslet
    {
        public Expression Accept(DwLangParser parser)
        {
            var openParenToken = parser.Take(TokenType.OpenParen);
            var expr = parser.ParseExpression();
            parser.Take(TokenType.CloseParen);

            return new Grouping(expr)
            {
                Token = openParenToken
            };
        }
    }
}
