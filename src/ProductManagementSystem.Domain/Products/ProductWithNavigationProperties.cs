using ProductManagementSystem.Currencies;

using System;
using System.Collections.Generic;

namespace ProductManagementSystem.Products
{
    public abstract class ProductWithNavigationPropertiesBase
    {
        public Product Product { get; set; } = null!;

        public Currency Currency { get; set; } = null!;
        

        
    }
}