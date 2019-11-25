using System;

namespace DwLang.Language
{
    public struct Token : IEquatable<Token>
    {
        public TokenType Type;
        public string Text;
        // TODO: Should this be evaluated and stored at lexer time or interpreter time?
        public object Value;

        public override bool Equals(object obj)
            => obj is Token token ? token.Equals(this) : false;

        public bool Equals(Token other)
            => other.Type == Type && other.Text == Text;

        public static bool operator ==(Token left, Token right)
            => left.Equals(right);

        public static bool operator !=(Token left, Token right)
            => !(left == right);

        public override int GetHashCode()
            => Text.GetHashCode();
    }
}