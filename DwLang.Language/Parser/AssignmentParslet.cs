using DwLang.Language.Expressions;

namespace DwLang.Language.Parser
{
    [Parslet(TokenType.Identifier, true)]
    public class AssignmentParslet : IParslet
    {
        public Expression Accept(DwLangParser parser)
        {
            var id = parser.Take(TokenType.Identifier);
            parser.Take(TokenType.Equals);

            var expr = parser.ParseExpression();
            var identifier = new Identifier(id.Text);

            return new Assignment(identifier, expr);
        }
    }
}
