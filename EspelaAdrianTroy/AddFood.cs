using System;
using System.Globalization;
using System.Windows.Forms;

namespace EspelaAdrianTroy
{
    public partial class AddFood : Form
    {
        public string ProductName { get; private set; } = string.Empty;
        public decimal ProductPrice { get; private set; }

        public AddFood()
        {
            InitializeComponent();
            button1.Click += Button1_Click;
        }

        private void Button1_Click(object? sender, EventArgs e)
        {
            var name = textBox1.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show(this, "Please enter a food name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(textBox2.Text, NumberStyles.Number | NumberStyles.AllowCurrencySymbol, CultureInfo.CurrentCulture, out var price))
            {
                MessageBox.Show(this, "Please enter a valid price (e.g. 4.99).", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ProductName = name;
            ProductPrice = price;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void AddFood_Load(object sender, EventArgs e)
        {

        }
    }
}
