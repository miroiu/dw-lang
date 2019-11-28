using DwLang.Language;
using System;
using System.Runtime.InteropServices;

namespace DwLang
{
    public class DwLangReplConsole : IOutputStream, IDisposable
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("Kernel32")]
        static extern void AllocConsole();

        [DllImport("Kernel32")]
        static extern void FreeConsole();

        private IntPtr _consoleHandle;
        private readonly string[] _arguments;

        public DwLangReplConsole(string[] arguments)
        {
            _arguments = arguments;
        }

        public void WriteLine(string line)
            => Console.WriteLine(line);

        public string[] GetArguments()
            => _arguments;

        public void Clear()
            => Console.Clear();

        public string ReadLine()
        {
            Console.Write("> ");
            return Console.ReadLine();
        }

        public void Write(string value)
            => Console.Write(value);

        public void Show()
        {
            if (_consoleHandle == IntPtr.Zero)
            {
                AllocConsole();
                _consoleHandle = GetConsoleWindow();
            }

            if (_consoleHandle != IntPtr.Zero)
            {
                ShowWindow(_consoleHandle, (int)Visibility.Show);
            }
        }

        public void Hide()
        {
            if (_consoleHandle != IntPtr.Zero)
            {
                ShowWindow(_consoleHandle, (int)Visibility.Hide);
            }
        }

        public void Dispose()
        {
            if (_consoleHandle != IntPtr.Zero)
            {
                FreeConsole();
            }
        }

        enum Visibility
        {
            Hide = 0,
            Show = 5
        }
    }
}
