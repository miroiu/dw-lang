namespace DwLang.Language.Expressions
{
    public class Identifier : Expression
    {
        public Identifier(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
