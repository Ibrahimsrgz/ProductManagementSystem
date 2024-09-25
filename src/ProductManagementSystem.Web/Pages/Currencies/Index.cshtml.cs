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
    public abstract class IndexModelBase : AbpPageModel
    {
        public string? CodeFilter { get; set; }
        public int? NumericFilterMin { get; set; }

        public int? NumericFilterMax { get; set; }
        public string? NameFilter { get; set; }
        public string? SymbolFilter { get; set; }
        public string? CountryFilter { get; set; }
        [SelectItems(nameof(ActiveBoolFilterItems))]
        public string ActiveFilter { get; set; }

        public List<SelectListItem> ActiveBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        public int? OrderFilterMin { get; set; }

        public int? OrderFilterMax { get; set; }

        protected ICurrenciesAppService _currenciesAppService;

        public IndexModelBase(ICurrenciesAppService currenciesAppService)
        {
            _currenciesAppService = currenciesAppService;
        }

        public virtual async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}