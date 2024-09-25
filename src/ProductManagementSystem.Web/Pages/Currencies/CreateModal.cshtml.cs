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
    public abstract class CreateModalModelBase : ProductManagementSystemPageModel
    {

        [BindProperty]
        public CurrencyCreateViewModel Currency { get; set; }

        protected ICurrenciesAppService _currenciesAppService;

        public CreateModalModelBase(ICurrenciesAppService currenciesAppService)
        {
            _currenciesAppService = currenciesAppService;

            Currency = new();
        }

        public virtual async Task OnGetAsync()
        {
            Currency = new CurrencyCreateViewModel();

            await Task.CompletedTask;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {

            await _currenciesAppService.CreateAsync(ObjectMapper.Map<CurrencyCreateViewModel, CurrencyCreateDto>(Currency));
            return NoContent();
        }
    }

    public class CurrencyCreateViewModel : CurrencyCreateDto
    {
    }
}