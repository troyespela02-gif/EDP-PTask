using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EspelaAdrianTroy.Models;

namespace EspelaAdrianTroy
{
    public partial class ProcessOrder : Form
    {
        private decimal? _paymentAmount;
        private decimal? _changeAmount;
        private readonly string? _customerName;

        public ProcessOrder() : this(Enumerable.Empty<Product>(), null) { }

        public ProcessOrder(IEnumerable<Product> initialProducts) : this(initialProducts, null) { }

        public ProcessOrder(IEnumerable<Product> initialProducts, string? customerName)
        {
            InitializeComponent();

            _customerName = string.IsNullOrWhiteSpace(customerName) ? null : customerName.Trim();

            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Clear();
            listView1.Columns.Add("Item", 220);
            listView1.Columns.Add("Price", 80, HorizontalAlignment.Right);


            button1.Click += Button1_AddItem;    // ADD
            button2.Click += Button2_RemoveItem; // REMOVE
            button4.Click += Button4_Pay;        // PAY
            button3.Click += Button3_Print;      // PRINT


            foreach (var p in initialProducts)
                AddProductToListView(p);

            UpdateTotalDisplay();
        }

        private void AddProductToListView(Product p)
        {
            var lvi = new ListViewItem(new[] { p.Name, p.Price.ToString("F2") }) { Tag = p };
            listView1.Items.Add(lvi);
        }

        private void Button1_AddItem(object? sender, EventArgs e)
        {
            var name = textBox1.Text?.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show(this, "Enter item name before adding.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var price = PromptForPrice(name);
            if (!price.HasValue) return;

            var product = new Product { Name = name, Price = price.Value };
            AddProductToListView(product);

            textBox1.Clear();
            UpdateTotalDisplay();
        }

        private void Button2_RemoveItem(object? sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                foreach (ListViewItem sel in listView1.SelectedItems)
                    listView1.Items.Remove(sel);

                UpdateTotalDisplay();
                return;
            }

            var name = textBox1.Text?.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show(this, "Select an item in the list or type its name to remove.", "Remove", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var toRemove = listView1.Items.Cast<ListViewItem>()
                .FirstOrDefault(i => string.Equals(i.SubItems[0].Text, name, StringComparison.OrdinalIgnoreCase));

            if (toRemove != null)
            {
                listView1.Items.Remove(toRemove);
                UpdateTotalDisplay();
            }
            else
            {
                MessageBox.Show(this, "Item not found.", "Remove", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Button4_Pay(object? sender, EventArgs e)
        {
            if (!decimal.TryParse(textBox2.Text, NumberStyles.Number | NumberStyles.AllowCurrencySymbol, CultureInfo.CurrentCulture, out var payment))
            {
                MessageBox.Show(this, "Enter a valid payment amount (e.g. 10.00).", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var total = CalculateTotal();
            if (total == 0m)
            {
                MessageBox.Show(this, "No items in the order.", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (payment < total)
            {
                MessageBox.Show(this, "Payment is less than the total.", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _paymentAmount = payment;
            _changeAmount = payment - total;

            label2.Text = $"Total : {total:C}";
            label3.Text = $"Payment : {payment:C}";
            label4.Text = $"Change : {_changeAmount.Value:C}";
        }

        private void Button3_Print(object? sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show(this, "There are no items to print.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var total = CalculateTotal();
            var payment = _paymentAmount;
            var change = _changeAmount;

            string baseName;
            if (!string.IsNullOrWhiteSpace(_customerName))
            {
                var invalids = Path.GetInvalidFileNameChars();
                var cleaned = new string(_customerName.Where(c => !invalids.Contains(c)).ToArray()).Trim();
                if (string.IsNullOrWhiteSpace(cleaned))
                    baseName = $"order_{DateTime.Now:yyyyMMdd_HHmmss}";
                else
                    baseName = $"{cleaned}_{DateTime.Now:yyyyMMdd_HHmmss}";
            }
            else
            {
                baseName = $"order_{DateTime.Now:yyyyMMdd_HHmmss}";
            }

            using var sfd = new SaveFileDialog
            {
                Title = "Save processed order",
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                FileName = $"{baseName}.txt",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (sfd.ShowDialog(this) != DialogResult.OK) return;

            var sb = new StringBuilder();
            sb.AppendLine("Processed Order");
            sb.AppendLine($"Date: {DateTime.Now}");
            if (!string.IsNullOrWhiteSpace(_customerName))
                sb.AppendLine($"Customer: {_customerName}");
            sb.AppendLine(new string('-', 40));
            sb.AppendLine("Items:");
            foreach (ListViewItem item in listView1.Items)
            {
                var itemName = item.SubItems[0].Text;
                var itemPrice = item.SubItems[1].Text;
                sb.AppendLine($"{itemName}\t{itemPrice}");
            }
            sb.AppendLine(new string('-', 40));
            sb.AppendLine($"Total: {total:C}");

            if (payment.HasValue)
            {
                sb.AppendLine($"Payment: {payment.Value:C}");
                sb.AppendLine($"Change: {change.Value:C}");
            }
            else
            {
                sb.AppendLine("Payment: (not recorded)");
            }

            try
            {
                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show(this, $"Order saved to:\n{sfd.FileName}", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);

                try
                {
                    var psi = new ProcessStartInfo(sfd.FileName) { UseShellExecute = true };
                    Process.Start(psi);
                }
                catch
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Unable to save file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal CalculateTotal()
        {
            decimal total = 0m;
            foreach (ListViewItem item in listView1.Items)
            {
                if (decimal.TryParse(item.SubItems[1].Text, NumberStyles.Number | NumberStyles.AllowCurrencySymbol, CultureInfo.CurrentCulture, out var p))
                    total += p;
            }
            return total;
        }

        private void UpdateTotalDisplay()
        {
            var total = CalculateTotal();
            label2.Text = $"Total : {total:C}";
            _paymentAmount = null;
            _changeAmount = null;
            label3.Text = "Payment :";
            label4.Text = "Change :";
            textBox2.Clear();
        }

        private decimal? PromptForPrice(string productName)
        {
            using var prompt = new Form
            {
                Width = 320,
                Height = 150,
                Text = "Enter price",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            };

            var lbl = new Label { Left = 10, Top = 10, Width = 280, Text = $"Enter price for \"{productName}\":" };
            var txt = new TextBox { Left = 10, Top = 35, Width = 280 };
            var ok = new Button { Text = "OK", DialogResult = DialogResult.OK, Left = 130, Width = 75, Top = 65 };
            var cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Left = 215, Width = 75, Top = 65 };

            prompt.Controls.Add(lbl);
            prompt.Controls.Add(txt);
            prompt.Controls.Add(ok);
            prompt.Controls.Add(cancel);
            prompt.AcceptButton = ok;
            prompt.CancelButton = cancel;

            if (prompt.ShowDialog(this) != DialogResult.OK) return null;

            if (decimal.TryParse(txt.Text, NumberStyles.Number | NumberStyles.AllowCurrencySymbol, CultureInfo.CurrentCulture, out var price))
                return price;

            MessageBox.Show(this, "Invalid price entered.", "Price", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }
    }
}
