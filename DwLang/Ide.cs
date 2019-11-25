using System.Drawing;
using System.Windows.Forms;

namespace DwLang
{
    public partial class Ide : Form
    {
        protected TextBox SourceCodeTb { get; set; }
        protected TextBox ResultTb { get; set; }
        protected Button RunBtn { get; set; }
        public Ide()
        {
            InitializeComponent();
            Text = "DwIde";
            MaximumSize = new Size(800, 450);
            MinimumSize = new Size(800, 450);
            SourceCodeTb = new TextBox
            {
                Multiline = true,
                Height = 250,
                Width = 800
            };
            Controls.Add(SourceCodeTb);
            ResultTb = new TextBox
            {
                Multiline = true,
                Height = 100,
                Width = 800,
                Location = new Point(0, 250)
            };
            Controls.Add(ResultTb);
            RunBtn = new Button
            {
                Location = new Point(1, 351),
                Text = "Run"
            };
            Controls.Add(RunBtn);
        }

    }
}
