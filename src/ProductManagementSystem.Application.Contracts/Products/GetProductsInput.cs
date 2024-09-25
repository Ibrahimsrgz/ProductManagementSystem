using Volo.Abp.Application.Dtos;
using System;

namespace ProductManagementSystem.Products
{
    public abstract class GetProductsInputBase : PagedAndSortedResultRequestDto
    {

        public string? FilterText { get; set; }

        public string? Name { get; set; }
        public string? Code { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public int? QuantityMin { get; set; }
        public int? QuantityMax { get; set; }

        public GetProductsInputBase()
        {

        }
    }
}