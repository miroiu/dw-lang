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
        Pow,
        Prm,
        Pwd,
        Equals,

        // Unary operators
        Print,
        Exclamation,
        Sqr,

        // Variable arguments operators
        Avg,
        Med
    }
}