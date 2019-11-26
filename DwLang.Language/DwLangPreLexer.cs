using System.Collections.Generic;
using System.Linq;

namespace DwLang.Language
{
    public class DwLangPreLexer
    {
        private struct Comment
        {
            public int Position;
            public bool IsOpening;
        }

        private readonly SourceText _code;

        public DwLangPreLexer(string code)
        {
            _code = new SourceText(code);
        }

        public SourceText Sanitize()
        {
            List<Comment> comments = new List<Comment>(4);

            do
            {
                if (_code.Current == '/' && _code.MoveNext() && _code.Current == '*')
                {
                    comments.Add(new Comment
                    {
                        Position = _code.Position - 1,
                        IsOpening = true
                    });
                }
                else if (_code.Current == '*' && _code.MoveNext() && _code.Current == '\\')
                {
                    comments.Add(new Comment
                    {
                        Position = _code.Position - 1,
                        IsOpening = false
                    });
                }
            }
            while (_code.MoveNext());

            return new SourceText(string.Empty);
        }
    }
}
