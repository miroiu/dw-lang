using DwLang.Language.Expressions;
using DwLang.Language.Parser;

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

                case "x":
                    return TokenType.X;
            }

            return TokenType.Identifier;
        }

        public static bool IsKeyword(this string value)
            => value.ToKeyword() != TokenType.Identifier;

        public static bool IsOperator(this TokenType type)
        {
            switch (type)
            {
                case TokenType.Sqr:
                case TokenType.Exclamation:
                case TokenType.Plus:
                case TokenType.Minus:
                case TokenType.X:
                case TokenType.Colon:
                case TokenType.Pow:
                case TokenType.Prm:
                case TokenType.Pwd:
                    return true;
            }

            return false;
        }

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

            throw new DwLangException($"{type} is not an unary operator.");
        }

        public static BinaryOperatorType ToBinaryOperatorType(this TokenType type)
        {
            switch (type)
            {
                case TokenType.Plus:
                    return BinaryOperatorType.Plus;

                case TokenType.Minus:
                    return BinaryOperatorType.Minus;

                case TokenType.X:
                    return BinaryOperatorType.Multiply;

                case TokenType.Colon:
                    return BinaryOperatorType.Divide;

                case TokenType.Pow:
                    return BinaryOperatorType.Pow;

                case TokenType.Prm:
                    return BinaryOperatorType.Prm;

                case TokenType.Pwd:
                    return BinaryOperatorType.Pwd;
            }

            throw new DwLangException($"{type} is not a binary operator.");
        }

        public static OperatorPrecedence ToOperatorPrecedence(this TokenType type)
        {
            switch (type)
            {
                case TokenType.Plus:
                case TokenType.Minus:
                    return OperatorPrecedence.Addition;

                case TokenType.X:
                case TokenType.Colon:
                case TokenType.Pow:
                case TokenType.Prm:
                case TokenType.Pwd:
                    return OperatorPrecedence.Multiplication;

                case TokenType.Sqr:
                case TokenType.Exclamation:
                    return OperatorPrecedence.Prefix;
            }

            throw new DwLangException($"{type} is not an operator.");
        }
    }
}
