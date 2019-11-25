namespace DwLang.Language
{
    public class DwLangLexerException : DwLangException
    {
        public DwLangLexerException(int line, int statement, string message) : base(message)
        {
        }
    }
}
