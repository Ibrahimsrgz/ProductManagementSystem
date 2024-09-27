using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.MauiClient.Dtos.Product
{
    public class ProductItemDto 
    {
        public ProductDto Product { get; set; }
        public CurrencyDto Currency { get; set; }
    }
}
