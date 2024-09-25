using ProductManagementSystem.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Products;

namespace ProductManagementSystem.Web.Pages.Products
{
    public abstract class CreateModalModelBase : ProductManagementSystemPageModel
    {

        [BindProperty]
        public ProductCreateViewModel Product { get; set; }

        public List<SelectListItem> CurrencyLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };

        protected IProductsAppService _productsAppService;

        public CreateModalModelBase(IProductsAppService productsAppService)
        {
            _productsAppService = productsAppService;

            Product = new();
        }

        public virtual async Task OnGetAsync()
        {
            Product = new ProductCreateViewModel();
            CurrencyLookupListRequired.AddRange((
                                    await _productsAppService.GetCurrencyLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {

            await _productsAppService.CreateAsync(ObjectMapper.Map<ProductCreateViewModel, ProductCreateDto>(Product));
            return NoContent();
        }
    }

    public class ProductCreateViewModel : ProductCreateDto
    {
    }
}