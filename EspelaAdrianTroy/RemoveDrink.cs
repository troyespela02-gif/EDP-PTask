using System;
using System.Windows.Forms;

namespace EspelaAdrianTroy
{
    public partial class RemoveDrink : Form
    {
        public string ProductNameToRemove { get; private set; } = string.Empty;

        public RemoveDrink()
        {
            InitializeComponent();
            button1.Click += Button1_Click;
        }

        private void Button1_Click(object? sender, EventArgs e)
        {
            var name = textBox1.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show(this, "Please enter the drink name to remove.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ProductNameToRemove = name;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void RemoveDrink_Load(object sender, EventArgs e)
        {

        }
    }
}
