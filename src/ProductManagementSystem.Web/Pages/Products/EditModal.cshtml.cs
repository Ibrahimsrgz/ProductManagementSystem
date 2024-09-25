using ProductManagementSystem.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ProductManagementSystem.Products;

namespace ProductManagementSystem.Web.Pages.Products
{
    public abstract class EditModalModelBase : ProductManagementSystemPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public long Id { get; set; }

        [BindProperty]
        public ProductUpdateViewModel Product { get; set; }

        public List<SelectListItem> CurrencyLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };

        protected IProductsAppService _productsAppService;

        public EditModalModelBase(IProductsAppService productsAppService)
        {
            _productsAppService = productsAppService;

            Product = new();
        }

        public virtual async Task OnGetAsync()
        {
            var productWithNavigationPropertiesDto = await _productsAppService.GetWithNavigationPropertiesAsync(Id);
            Product = ObjectMapper.Map<ProductDto, ProductUpdateViewModel>(productWithNavigationPropertiesDto.Product);

            CurrencyLookupListRequired.AddRange((
                                    await _productsAppService.GetCurrencyLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {

            await _productsAppService.UpdateAsync(Id, ObjectMapper.Map<ProductUpdateViewModel, ProductUpdateDto>(Product));
            return NoContent();
        }
    }

    public class ProductUpdateViewModel : ProductUpdateDto
    {
    }
}