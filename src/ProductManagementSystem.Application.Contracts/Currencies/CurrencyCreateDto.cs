using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProductManagementSystem.Currencies
{
    public abstract class CurrencyCreateDtoBase
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
    }
}