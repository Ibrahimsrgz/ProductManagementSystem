using System;

namespace ProductManagementSystem.Currencies
{
    public abstract class CurrencyExcelDtoBase
    {
        public string Code { get; set; } = null!;
        public int Numeric { get; set; }
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public string? Country { get; set; }
        public bool Active { get; set; }
        public int Order { get; set; }
    }
}