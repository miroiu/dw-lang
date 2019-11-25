using System.Threading.Tasks;

namespace DwLang.Language
{
    public class DwLangInterpreter
    {
        public async Task<string> Run(Expression root)
        {
            await Task.CompletedTask;
            // TODO
            return "some-output";
        }
    }
}
