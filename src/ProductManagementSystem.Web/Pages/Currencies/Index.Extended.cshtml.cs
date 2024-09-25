using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ProductManagementSystem.Currencies;
using ProductManagementSystem.Shared;

namespace ProductManagementSystem.Web.Pages.Currencies
{
    public class IndexModel : IndexModelBase
    {
        public IndexModel(ICurrenciesAppService currenciesAppService)
            : base(currenciesAppService)
        {
        }
    }
}