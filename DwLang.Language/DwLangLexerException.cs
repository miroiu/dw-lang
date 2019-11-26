namespace DwLang.Language
{
    public class DwLangLexerException : DwLangException
    {
        public DwLangLexerException(int line, int column, string message) : base(message)
        {
            Line = line;
            Column = column;
        }

        public int Line { get; }
        public int Column { get; }
    }
}
