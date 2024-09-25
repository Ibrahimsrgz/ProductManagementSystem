using ProductManagementSystem.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ProductManagementSystem.Currencies;

namespace ProductManagementSystem.Web.Pages.Currencies
{
    public abstract class EditModalModelBase : ProductManagementSystemPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CurrencyUpdateViewModel Currency { get; set; }

        protected ICurrenciesAppService _currenciesAppService;

        public EditModalModelBase(ICurrenciesAppService currenciesAppService)
        {
            _currenciesAppService = currenciesAppService;

            Currency = new();
        }

        public virtual async Task OnGetAsync()
        {
            var currency = await _currenciesAppService.GetAsync(Id);
            Currency = ObjectMapper.Map<CurrencyDto, CurrencyUpdateViewModel>(currency);

        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {

            await _currenciesAppService.UpdateAsync(Id, ObjectMapper.Map<CurrencyUpdateViewModel, CurrencyUpdateDto>(Currency));
            return NoContent();
        }
    }

    public class CurrencyUpdateViewModel : CurrencyUpdateDto
    {
    }
}