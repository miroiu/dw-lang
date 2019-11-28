using DwLang.Language;
using System;

namespace DwLang
{
    public class DwLangReplConsole : IOutputStream
    {
        public void WriteLine(string line)
            => Console.WriteLine(line);

        public void Clear()
            => Console.Clear();

        public string ReadLine()
            => Console.ReadLine();
    }
}
