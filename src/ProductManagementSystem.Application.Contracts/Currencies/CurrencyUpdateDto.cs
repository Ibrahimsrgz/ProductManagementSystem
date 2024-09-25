using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ProductManagementSystem.Currencies
{
    public abstract class CurrencyUpdateDtoBase : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(CurrencyConsts.CodeMaxLength)]
        public string Code { get; set; } = null!;
        public int Numeric { get; set; }
        [StringLength(CurrencyConsts.NameMaxLength)]
        public string? Name { get; set; }
        [StringLength(CurrencyConsts.SymbolMaxLength)]
        public string? Symbol { get; set; }
        [StringLength(CurrencyConsts.CountryMaxLength)]
        public string? Country { get; set; }
        public bool Active { get; set; }
        public int Order { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}