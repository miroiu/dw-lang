using System.Collections.Generic;

namespace DwLang.Language
{
    public class DwLangParser
    {
        private readonly DwLangLexer _lexer;

        public DwLangParser(DwLangLexer lexer)
        {
            _lexer = lexer;
        }

        public Expression Parse()
        {
            return default;
        }
    }
}
