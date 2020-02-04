namespace DwLang.Editor
{
    public class DwLangTreeNode
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
        public DwLangTreeNode[] Children { get; set; }
    }
}
