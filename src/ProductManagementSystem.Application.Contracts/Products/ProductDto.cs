using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ProductManagementSystem.Products
{
    public abstract class ProductDtoBase : FullAuditedEntityDto<long>, IHasConcurrencyStamp
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid CurrencyId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}