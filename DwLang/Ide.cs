using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DwLang
{
    public partial class Ide : Form
    {
        public TextBox SourceCodeTb { get; private set; }
        public TextBox ResultTb { get; private set; }
        public Button RunBtn { get; private set; }

        private readonly DwLangCompiler _compiler;
        private readonly DwLangInterpreter _interpreter;
        
        public Ide()
        {
            _compiler = new DwLangCompiler();
            _interpreter = new DwLangInterpreter();
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

            RunBtn.Click += RunBtn_Click;
        }

        private void RunBtn_Click(object sender, System.EventArgs e)
        {
            Task.Run(async () =>
            {
                ResultTb.Text = "Compiling...";
                try
                {
                    var compilationOutput = await _compiler.Compile(SourceCodeTb.Text);
                    ResultTb.Text = "Compiled. Running...";
                    var output = await _interpreter.Run(compilationOutput);
                    ResultTb.Text = output;
                }
                catch (DwLangException ex)
                {
                    ResultTb.ForeColor = Color.Red;
                    ResultTb.Text = "Compilation error: " + ex.Message;
                }
            });
        }
    }
}
