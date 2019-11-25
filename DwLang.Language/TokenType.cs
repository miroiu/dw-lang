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

        // Directives
        Set,
        Precision,

        // Binary operators
        Plus,
        Minus,
        X,
        Colon,
        Equals,
        Pow,
        Prm,
        Pwd,

        // Unary operators
        Print,
        Exclamation,
        Sqr,

        // Variable arguments operators
        Avg,
        Med
    }
}