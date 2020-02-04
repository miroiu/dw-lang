using DwLang.Language.Expressions;
using DwLang.Language.Lexer;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Cls, true)]
    public class CommandParslet : IParslet
    {
        public Expression Accept(DwLangParser parser)
        {
            var opToken = parser.Take(TokenType.Cls);

            return new CommandExpression(opToken.ToCommandType())
            {
                Token = opToken
            };
        }
    }
}
