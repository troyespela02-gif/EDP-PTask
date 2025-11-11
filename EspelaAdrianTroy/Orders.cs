using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EspelaAdrianTroy.Models;

namespace EspelaAdrianTroy
{
    public partial class Orders : Form
    {
        private readonly List<Product> _products;

        public Orders() : this(new List<Product>()) { }

        public Orders(List<Product> products)
        {
            InitializeComponent();
            _products = products ?? new List<Product>();

            SetupListViews();
            RefreshLists();

            button1.Click += Button1_Click; 

            listView1.MultiSelect = true;
            listView2.MultiSelect = true;
        }

        private void SetupListViews()
        {

            listView2.View = View.Details;
            listView2.FullRowSelect = true;
            listView2.Columns.Clear();
            listView2.Columns.Add("Name", 140);
            listView2.Columns.Add("Price", 80, HorizontalAlignment.Right);

            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Clear();
            listView1.Columns.Add("Name", 140);
            listView1.Columns.Add("Price", 80, HorizontalAlignment.Right);
        }

        private void RefreshLists()
        {
            listView2.Items.Clear();
            listView1.Items.Clear();

            foreach (var p in _products)
            {
                var lvi = new ListViewItem(new[] { p.Name, p.Price.ToString("F2") }) { Tag = p };
                if (p.Type == ProductType.Food)
                    listView2.Items.Add(lvi);
                else
                    listView1.Items.Add(lvi);
            }
        }

        private void Button1_Click(object? sender, EventArgs e)
        {
            var selected = new List<Product>();

            foreach (ListViewItem lv in listView2.SelectedItems)
                if (lv.Tag is Product p) selected.Add(p);

            foreach (ListViewItem lv in listView1.SelectedItems)
                if (lv.Tag is Product p) selected.Add(p);

            if (selected.Count == 0)
            {
                MessageBox.Show(this, "Please select at least one item to order.", "Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var customerName = PromptForCustomerName();
            if (string.IsNullOrWhiteSpace(customerName))
            {
                // user cancelled or entered empty — don't proceed
                return;
            }

            using var proc = new ProcessOrder(selected, customerName);
            proc.ShowDialog(this);
        }

        private string? PromptForCustomerName()
        {
            using var prompt = new Form
            {
                Width = 380,
                Height = 150,
                Text = "Customer Name",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            };

            var lbl = new Label { Left = 12, Top = 10, Width = 340, Text = "Enter customer name:" };
            var txt = new TextBox { Left = 12, Top = 35, Width = 340 };
            var ok = new Button { Text = "OK", DialogResult = DialogResult.OK, Left = 200, Width = 75, Top = 65 };
            var cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Left = 285, Width = 75, Top = 65 };

            prompt.Controls.Add(lbl);
            prompt.Controls.Add(txt);
            prompt.Controls.Add(ok);
            prompt.Controls.Add(cancel);
            prompt.AcceptButton = ok;
            prompt.CancelButton = cancel;

            if (prompt.ShowDialog(this) != DialogResult.OK) return null;
            return txt.Text.Trim();
        }

        private void Orders_Load(object sender, EventArgs e)
        {
        }
    }
}
