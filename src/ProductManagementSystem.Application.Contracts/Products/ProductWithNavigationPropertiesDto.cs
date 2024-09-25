using ProductManagementSystem.Currencies;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ProductManagementSystem.Products
{
    public abstract class ProductWithNavigationPropertiesDtoBase
    {
        public ProductDto Product { get; set; } = null!;

        public CurrencyDto Currency { get; set; } = null!;

    }
}