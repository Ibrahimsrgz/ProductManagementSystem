using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using ProductManagementSystem.Permissions;
using ProductManagementSystem.Currencies;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ProductManagementSystem.Shared;

namespace ProductManagementSystem.Currencies
{
    public class CurrenciesAppService : CurrenciesAppServiceBase, ICurrenciesAppService
    {
        //<suite-custom-code-autogenerated>
        public CurrenciesAppService(ICurrencyRepository currencyRepository, CurrencyManager currencyManager, IDistributedCache<CurrencyDownloadTokenCacheItem, string> downloadTokenCache)
            : base(currencyRepository, currencyManager, downloadTokenCache)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}