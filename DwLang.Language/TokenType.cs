namespace DwLang.Language
{
    public enum TokenType
    {
        EndOfFile,
        EndOfLine,

        Identifier,
        Number,

        // Separators
        Comma,
        Semicolon,
        CloseParen,
        OpenParen,

        // Keywords
        Var,
        Pow,
        Prm,
        Pwd,
        Sqr,
        Avg,
        Med,
        Print,

        // Directives
        SetPrecision,

        // Operators
        Plus,
        Minus,
        Asterisk,
        Slash,
        Exclamation,
        Equals
    }
}