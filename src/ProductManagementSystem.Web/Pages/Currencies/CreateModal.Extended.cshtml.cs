using ProductManagementSystem.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Currencies;

namespace ProductManagementSystem.Web.Pages.Currencies
{
    public class CreateModalModel : CreateModalModelBase
    {
        public CreateModalModel(ICurrenciesAppService currenciesAppService)
            : base(currenciesAppService)
        {
        }
    }
}