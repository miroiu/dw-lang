using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DwLang.Language
{
    public class DwLangInterpreter
    {
        private readonly IOutputStream _output;

        public DwLangInterpreter(IOutputStream output)
        {
            _output = output;
        }

        public async Task Run(Expression root)
        {
            await Task.CompletedTask;

            _output.WriteLine("some-output");
        }
    }
}
