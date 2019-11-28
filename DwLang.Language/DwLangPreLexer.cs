using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DwLang.Language.Parser;

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
            return new SourceText(StripComments(_code.Text));
        }

        private static string StripComments(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }
            if (source.Length <= 3)
            {
                return source;
            }

            var newLinesPositions = new HashSet<int>();
            int k = 0;

            while (source.IndexOf(Environment.NewLine, k + 1) > -1)
            {
                k = source.IndexOf(Environment.NewLine, k + 1);
                newLinesPositions.Add(k);
            }

            source = source.Replace(Environment.NewLine, string.Empty);

            int? openedAt = null;
            int? possiblyClosedAt = null;
            for (var i = 0; i < source.Length; i++)
            {
                // OPENED
                if (i < source.Length - 1 && source[i] == '/' && source[i + 1] == '*')
                {
                    if (openedAt != null && possiblyClosedAt != null)
                    {
                        source = ReplaceCommentWithSpaces(source, openedAt.Value, possiblyClosedAt.Value);
                        possiblyClosedAt = null;
                        openedAt = null;
                    }
                    if (openedAt == null && possiblyClosedAt == null)
                    {
                        openedAt = i;
                        continue;
                    }
                }

                // CLOSED
                if (i > 0 && source[i] == '\\' && source[i - 1] == '*')
                {
                    if (openedAt != null)
                    {
                        possiblyClosedAt = i;
                        continue;
                    }
                }
            }
            if (openedAt != null)
            {
                if (possiblyClosedAt != null)
                {
                    source = ReplaceCommentWithSpaces(source, openedAt.Value, possiblyClosedAt.Value);
                }
                else
                {
                    var token = new Token();
                    var subStr = source.Substring(0, openedAt.Value);
                    token.Line = subStr.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Count() - 1;
                    token.Column = openedAt.Value - subStr.LastIndexOf(Environment.NewLine);
                    throw new DwLangParserException(token, "Invalid comment");
                }
            }

            if (newLinesPositions.Any())
            {
                var sb = new StringBuilder(source);
                foreach (var pos in newLinesPositions)
                {
                    sb.Insert(pos, Environment.NewLine);
                }
                source = sb.ToString();
            }

            return source;
        }

        private static string ReplaceCommentWithSpaces(string source, int start, int end)
        {
            return new StringBuilder(source)
                .Remove(start, end - start + 1)
                .Insert(start, "".PadLeft(end - start + 1, ' '))
                .ToString();
        }
    }
}
