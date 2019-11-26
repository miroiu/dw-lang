using NUnit.Framework;

namespace DwLang.Tests
{
    public class ParserTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, TestCase("print")]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}