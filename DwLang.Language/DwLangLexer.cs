﻿namespace DwLang.Language
{
    public partial class DwLangLexer
    {
        private readonly SourceText _text;
        private int _statementIndex;
        private int _lineNumber;
        private TokenType _previousToken;

        public DwLangLexer(SourceText text)
        {
            _text = text;
        }

        public Token Lex()
        {
            Token token = new Token();

            switch (_text.Current)
            {
                case '\0':
                    token.Type = TokenType.EndOfCode;
                    break;

                case ';':
                    token.Type = TokenType.Semicolon;
                    _text.MoveNext();
                    ++_statementIndex;
                    break;

                case '+':
                    token.Type = TokenType.Plus;
                    _text.MoveNext();
                    break;

                case '-':
                    token.Type = TokenType.Minus;
                    _text.MoveNext();
                    break;

                case ':':
                    token.Type = TokenType.Colon;
                    _text.MoveNext();
                    break;

                case '(':
                    token.Type = TokenType.OpenParen;
                    _text.MoveNext();
                    break;

                case ')':
                    token.Type = TokenType.CloseParen;
                    _text.MoveNext();
                    break;

                case '!':
                    token.Type = TokenType.Exclamation;
                    _text.MoveNext();
                    break;

                case '=':
                    token.Type = TokenType.Equals;
                    _text.MoveNext();
                    break;

                case ' ':
                case '\t':
                    ReadWhiteSpace(_text);
                    return Lex();

                case '\r':
                    _text.MoveNext();
                    return Lex();

                case '\n':
                    ++_lineNumber;
                    _text.MoveNext();
                    return Lex();

                case '/':
                    ReadComment(_text);
                    break;

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    token.Type = TokenType.Number;
                    token.Text = ReadNumber(_text);
                    break;

                case 'a':
                case 'b':
                case 'c':
                case 'd':
                case 'e':
                case 'f':
                case 'g':
                case 'h':
                case 'i':
                case 'j':
                case 'k':
                case 'l':
                case 'm':
                case 'n':
                case 'o':
                case 'p':
                case 'q':
                case 'r':
                case 's':
                case 't':
                case 'u':
                case 'v':
                case 'w':
                case 'x':
                case 'y':
                case 'z':
                case 'A':
                case 'B':
                case 'C':
                case 'D':
                case 'E':
                case 'F':
                case 'G':
                case 'H':
                case 'I':
                case 'J':
                case 'K':
                case 'L':
                case 'M':
                case 'N':
                case 'O':
                case 'P':
                case 'Q':
                case 'R':
                case 'S':
                case 'T':
                case 'U':
                case 'V':
                case 'W':
                case 'X':
                case 'Y':
                case 'Z':
                case '_':
                    if (_text.Current == 'x' && _previousToken == TokenType.Number)
                    {
                        token.Type = TokenType.X;
                        break;
                    }

                    var (Type, Text) = ReadIdentifierOrKeyword(_text);
                    token.Type = Type;
                    token.Text = Text;
                    break;

                default:
                    throw new DwLangLexerException(_lineNumber, _statementIndex, $"Unexpected character {_text.Current}");
            }

            return token;
        }
    }
}
