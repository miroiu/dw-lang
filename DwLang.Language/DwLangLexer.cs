namespace DwLang.Language
{
    public class DwLangLexer
    {
        private readonly SourceText _text;

        public DwLangLexer(SourceText text)
        {
            _text = text;
        }

        public Token Lex()
        {
            return default;
        }
    }
}
