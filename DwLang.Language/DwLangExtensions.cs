using DwLang.Language.Expressions;

namespace DwLang.Language
{
    public static class DwLangExtensions
    {
        public static TokenType ToKeyword(this string value)
        {
            switch (value)
            {
                case "var":
                    return TokenType.Var;

                case "pow":
                    return TokenType.Pow;

                case "prm":
                    return TokenType.Prm;

                case "pwd":
                    return TokenType.Pwd;

                case "sqr":
                    return TokenType.Sqr;

                case "avg":
                    return TokenType.Avg;

                case "med":
                    return TokenType.Med;

                case "print":
                    return TokenType.Print;

                case "set":
                    return TokenType.Set;

                case "precision":
                    return TokenType.Precision;
            }

            return TokenType.Identifier;
        }

        public static bool IsKeyword(this string value)
            => value.ToKeyword() != TokenType.Identifier;

        public static UnaryOperatorType ToUnaryOperatorType(this TokenType type)
        {
            switch (type)
            {
                case TokenType.Print:
                    return UnaryOperatorType.Print;

                case TokenType.Sqr:
                    return UnaryOperatorType.Sqr;

                case TokenType.Exclamation:
                    return UnaryOperatorType.Factorial;
            }

            throw new DwLangException($"{type} is not unary operator.");
        }
    }
}
