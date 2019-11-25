using DwLang.Language.Expressions;

namespace DwLang.Language.Parser
{
    public interface IParslet
    {
        Expression Accept(DwLangParser parser, Token token);
    }
}
