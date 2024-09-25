using System;

namespace ProductManagementSystem.Products
{
    public abstract class ProductExcelDtoBase
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}