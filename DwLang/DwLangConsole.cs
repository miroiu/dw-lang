using System.Text;

namespace DwLang
{
    public class DwLangConsole : DwLangObservable
    {
        private readonly StringBuilder _string = new StringBuilder(128);

        public string Output
            => _string.ToString();

        private string _input;
        public string Input
        {
            get => _input;
            set => SetProperty(ref _input, value);
        }

        public void WriteLine(string code)
        {
            _string.AppendLine($"> {code}");

            OnPropertyChanged(nameof(Output));
        }

        public string ReadLine()
        {
            var input = Input;
            Input = default;
            return input;
        }
    }
}
