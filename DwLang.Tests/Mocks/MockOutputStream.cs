using DwLang.Language;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwLang.Tests.Mocks
{
    public class MockOutputStream : IOutputStream
    {
        private readonly StringBuilder _output = new StringBuilder();

        public void WriteLine(string line)
        {
            _output.AppendLine(line);
        }

        public string CurrentOutput
        {
            get
            {
                return _output.ToString();
            }
        }
    }
}
