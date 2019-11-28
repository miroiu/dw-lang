using DwLang.Editor;
using System;
using System.Linq;

namespace DwLang
{
    class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                App app = new App();
                app.Run(new MainWindow());
            }
            else
            {
                var console = new DwLangConsole(args.Skip(1).ToArray());
                console.RunCommand(args[0]);
            }
        }
    }
}
