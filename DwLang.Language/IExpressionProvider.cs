using DwLang.Language.Expressions;

namespace DwLang.Language
{
    public interface IExpressionProvider
    {
        bool HasNext { get; }

        Expression Next();
    }
}