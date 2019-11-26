using System.Collections;
using System.Collections.Generic;

namespace DwLang.Language
{
    public class SourceText : IEnumerator<char>
    {
        private readonly string _source;

        public string Text
        {
            get
            {
                return _source;
            }
        }

        public int Position { get; set; }
        public int Length => _source.Length;

        public SourceText(string source)
            => _source = $"{source}\0";

        public char Current => _source[Position];

        object IEnumerator.Current => Current;

        public int Column { get; private set; }
        public int Line { get; private set; }

        public bool MoveNext()
        {
            if (Position + 1 < _source.Length)
            {
                Position++;
                Column++;

                if (Current == '\n')
                {
                    Line++;
                    Column = 0;
                }

                return true;
            }

            return false;
        }

        public char Peek(int count = 1)
        {
            var location = Position + count;

            if (location < _source.Length && location >= 0)
            {
                return _source[location];
            }

            return '\0';
        }

        public void Reset()
            => Position = 0;

        public void Dispose()
            => Reset();
    }
}
