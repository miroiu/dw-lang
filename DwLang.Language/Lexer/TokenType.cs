namespace DwLang.Language.Lexer
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
        Cls,

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