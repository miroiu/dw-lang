using System.Collections;
using System.Collections.Generic;

namespace DwLang.Language
{
    public class SourceText : IEnumerator<char>
    {
        private readonly string _source;

        public int Position { get; private set; }

        public SourceText(string source)
            => _source = source;

        public char Current => _source[Position];

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (Position + 1 < _source.Length)
            {
                Position++;
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
