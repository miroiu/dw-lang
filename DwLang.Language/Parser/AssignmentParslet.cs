using DwLang.Language.Expressions;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Identifier, true)]
    public class AssignmentParslet : IParslet
    {
        public Expression Accept(DwLangParser parser, Token token)
        {
            parser.Match(TokenType.Equals);
            var expr = parser.ParseExpression();
            var identifier = new Identifier(token.Text);
            return new Assignment(identifier, expr);
        }
    }
}
