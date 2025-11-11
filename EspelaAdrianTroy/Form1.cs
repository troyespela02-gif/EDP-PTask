using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EspelaAdrianTroy.Models;

namespace EspelaAdrianTroy
{
    public partial class Form1 : Form
    {
        private readonly List<Product> _products;

        public Form1()
        {
            InitializeComponent();

            _products = new List<Product>
            {

                new Product { Name = "Burger", Price = 5.50m, Type = ProductType.Food },
                new Product { Name = "Fries", Price = 2.25m, Type = ProductType.Food },
                new Product { Name = "Coke", Price = 1.50m, Type = ProductType.Drink }
            };

            button1.Click += ButtonViewProducts_Click; 
            button2.Click += ButtonOrder_Click;        
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ButtonViewProducts_Click(object? sender, EventArgs e)
        {
            using var p = new Products(_products);
            p.ShowDialog(this);
        }

        private void ButtonOrder_Click(object? sender, EventArgs e)
        {
            using var o = new Orders(_products);
            o.ShowDialog(this);
        }
    }
}
