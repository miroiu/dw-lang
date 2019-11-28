using System.Text;

namespace DwLang.Language
{
    public partial class DwLangLexer
    {
        private (TokenType Type, string Text) ReadIdentifierOrKeyword(SourceText stream)
        {
            StringBuilder builder = new StringBuilder(12);

            while (char.IsLetter(stream.Current) || stream.Current == '_')
            {
                builder.Append(stream.Current);
                stream.MoveNext();
            }

            var text = builder.ToString();

            return (text.ToKeyword(), text);
        }

        private string ReadNumber(SourceText stream)
        {
            StringBuilder builder = new StringBuilder(8);
            bool hasComma = false;

            while (true)
            {
                if (stream.Current == ',')
                {
                    if (!hasComma)
                    {
                        hasComma = true;

                        builder.Append(stream.Current);
                        stream.MoveNext();
                    }
                    else
                    {
                        throw new DwLangLexerException(_text.Line, _text.Column, $"Invalid number format.");
                    }
                }
                else if (char.IsDigit(stream.Current))
                {
                    builder.Append(stream.Current);
                    stream.MoveNext();
                }
                else
                {
                    break;
                }
            }
            if (stream.Peek(-1) == ',')
            {
                throw new DwLangLexerException(_text.Line, _text.Column, $"Invalid number format.");
            }
            return builder.ToString();
        }

        private void ReadWhiteSpace(SourceText stream)
        {
            while (char.IsWhiteSpace(stream.Current) && stream.MoveNext()) ;
        }
    }
}
