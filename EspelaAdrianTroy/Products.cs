using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EspelaAdrianTroy.Models;

namespace EspelaAdrianTroy
{
    public partial class Products : Form
    {
        private readonly List<Product> _products;

        public Products() : this(new List<Product>()) { }

        public Products(List<Product> products)
        {
            InitializeComponent();
            _products = products ?? new List<Product>();

            SetupListViews();
            RefreshLists();

            button1.Click += BtnAddFood_Click;
            button2.Click += BtnRemoveFood_Click;
            button3.Click += BtnAddDrink_Click;
            button4.Click += BtnRemoveDrink_Click;
        }

        private void SetupListViews()
        {
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Clear();
            listView1.Columns.Add("Name", 200);
            listView1.Columns.Add("Price", 80, HorizontalAlignment.Right);

            listView2.View = View.Details;
            listView2.FullRowSelect = true;
            listView2.Columns.Clear();
            listView2.Columns.Add("Name", 200);
            listView2.Columns.Add("Price", 80, HorizontalAlignment.Right);
        }

        private void RefreshLists()
        {
            listView1.Items.Clear(); // foods
            listView2.Items.Clear(); // drinks

            foreach (var p in _products)
            {
                var lvi = new ListViewItem(new[] { p.Name, p.Price.ToString("F2") }) { Tag = p };
                if (p.Type == ProductType.Food)
                    listView1.Items.Add(lvi);
                else
                    listView2.Items.Add(lvi);
            }
        }

        private void BtnAddFood_Click(object? sender, EventArgs e)
        {
            using var dlg = new AddFood();
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            var product = new Product
            {
                Name = dlg.ProductName,
                Price = dlg.ProductPrice,
                Type = ProductType.Food
            };

            _products.Add(product);
            RefreshLists();
        }

        private void BtnAddDrink_Click(object? sender, EventArgs e)
        {
            using var dlg = new AddDrink();
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            var product = new Product
            {
                Name = dlg.ProductName,
                Price = dlg.ProductPrice,
                Type = ProductType.Drink
            };

            _products.Add(product);
            RefreshLists();
        }

        private void BtnRemoveFood_Click(object? sender, EventArgs e)
        {
            using var dlg = new RemoveFood();
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            var name = dlg.ProductNameToRemove;
            var item = _products.FirstOrDefault(p => p.Type == ProductType.Food &&
                                                    string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
            if (item == null)
            {
                MessageBox.Show(this, "Food not found.", "Remove", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _products.Remove(item);
            RefreshLists();
        }

        private void BtnRemoveDrink_Click(object? sender, EventArgs e)
        {
            using var dlg = new RemoveDrink();
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            var name = dlg.ProductNameToRemove;
            var item = _products.FirstOrDefault(p => p.Type == ProductType.Drink &&
                                                    string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
            if (item == null)
            {
                MessageBox.Show(this, "Drink not found.", "Remove", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _products.Remove(item);
            RefreshLists();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
