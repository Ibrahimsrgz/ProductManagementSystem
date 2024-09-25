using Volo.Abp.Application.Dtos;
using System;

namespace ProductManagementSystem.Currencies
{
    public abstract class CurrencyExcelDownloadDtoBase
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public string? Code { get; set; }
        public int? NumericMin { get; set; }
        public int? NumericMax { get; set; }
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public string? Country { get; set; }
        public bool? Active { get; set; }
        public int? OrderMin { get; set; }
        public int? OrderMax { get; set; }

        public CurrencyExcelDownloadDtoBase()
        {

        }
    }
}