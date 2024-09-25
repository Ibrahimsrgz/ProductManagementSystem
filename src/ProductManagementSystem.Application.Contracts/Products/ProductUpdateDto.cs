using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ProductManagementSystem.Products
{
    public abstract class ProductUpdateDtoBase : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(ProductConsts.NameMaxLength)]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(ProductConsts.CodeMaxLength)]
        public string Code { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid CurrencyId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}