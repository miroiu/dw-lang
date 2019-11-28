using System.Collections;
using System.Collections.Generic;

namespace DwLang.Language
{
    public class SourceText : IEnumerator<char>
    {
        public string Text { get; }

        public int Position { get; set; }
        public int Length => Text.Length;

        public SourceText(string source)
            => Text = $"{source}\0";

        public char Current => Text[Position];

        object IEnumerator.Current => Current;

        public int Column { get; set; }
        public int Line { get; set; }

        public bool MoveNext()
        {
            if (Position + 1 < Text.Length)
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

            if (location < Text.Length && location >= 0)
            {
                return Text[location];
            }

            return '\0';
        }

        public void Reset()
            => Position = 0;

        public void Dispose()
            => Reset();
    }
}
