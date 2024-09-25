using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ProductManagementSystem.Currencies
{
    public abstract class CurrencyDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; } = null!;
        public int Numeric { get; set; }
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public string? Country { get; set; }
        public bool Active { get; set; }
        public int Order { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}