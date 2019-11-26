namespace DwLang.Language.Parser
{
    public class DwLangParserException : DwLangException
    {
        public DwLangParserException(Token token, string message) : base(message)
        {
            Token = token;
        }

        public Token Token { get; }
    }
}
