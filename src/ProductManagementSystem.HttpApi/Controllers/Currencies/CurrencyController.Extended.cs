using Asp.Versioning;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using ProductManagementSystem.Currencies;

namespace ProductManagementSystem.Controllers.Currencies
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Currency")]
    [Route("api/app/currencies")]

    public class CurrencyController : CurrencyControllerBase, ICurrenciesAppService
    {
        public CurrencyController(ICurrenciesAppService currenciesAppService) : base(currenciesAppService)
        {
        }
    }
}