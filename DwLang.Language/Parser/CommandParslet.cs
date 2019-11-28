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

            return new Command(opToken.ToCommandType())
            {
                Token = opToken
            };
        }
    }
}
