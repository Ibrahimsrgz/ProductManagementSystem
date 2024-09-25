using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ProductManagementSystem.Products;
using ProductManagementSystem.Shared;

namespace ProductManagementSystem.Web.Pages.Products
{
    public abstract class IndexModelBase : AbpPageModel
    {
        public string? NameFilter { get; set; }
        public string? CodeFilter { get; set; }
        public decimal? PriceFilterMin { get; set; }

        public decimal? PriceFilterMax { get; set; }
        public int? QuantityFilterMin { get; set; }

        public int? QuantityFilterMax { get; set; }
        [SelectItems(nameof(CurrencyLookupList))]
        public Guid CurrencyIdFilter { get; set; }
        public List<SelectListItem> CurrencyLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        protected IProductsAppService _productsAppService;

        public IndexModelBase(IProductsAppService productsAppService)
        {
            _productsAppService = productsAppService;
        }

        public virtual async Task OnGetAsync()
        {
            CurrencyLookupList.AddRange((
                    await _productsAppService.GetCurrencyLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            await Task.CompletedTask;
        }
    }
}