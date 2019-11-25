namespace DwLang.Language
{
    public enum TokenType
    {
        EndOfCode,

        Identifier,
        Number,

        // Separators
        Semicolon,
        CloseParen,
        OpenParen,
        Comment,

        // Keywords
        Var,
        Print,

        // Directives
        Set,
        Precision,

        // Operators
        Plus,
        Minus,
        X,
        Colon,
        Exclamation,
        Equals,

        Pow,
        Prm,
        Pwd,
        Sqr,
        Avg,
        Med
    }
}