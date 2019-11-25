using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DwLang
{
    public class DwLangCompiler
    {
        public async Task<object> Compile(string sourceCode)
        {
            await Task.CompletedTask;
            var sourceText = new SourceText(sourceCode);
            // TODO
            return null;
        }
    }
}
