using System;

namespace EspelaAdrianTroy.Models
{
    public enum ProductType
    {
        Food,
        Drink
    }

    public sealed class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ProductType Type { get; set; }
    }
}