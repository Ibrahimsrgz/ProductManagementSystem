using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ProductManagementSystem.Currencies
{
    public abstract class CurrencyManagerBase : DomainService
    {
        protected ICurrencyRepository _currencyRepository;

        public CurrencyManagerBase(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public virtual async Task<Currency> CreateAsync(
        string code, int numeric, bool active, int order, string? name = null, string? symbol = null, string? country = null)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), CurrencyConsts.CodeMaxLength);
            Check.Length(name, nameof(name), CurrencyConsts.NameMaxLength);
            Check.Length(symbol, nameof(symbol), CurrencyConsts.SymbolMaxLength);
            Check.Length(country, nameof(country), CurrencyConsts.CountryMaxLength);

            var currency = new Currency(
             GuidGenerator.Create(),
             code, numeric, active, order, name, symbol, country
             );

            return await _currencyRepository.InsertAsync(currency);
        }

        public virtual async Task<Currency> UpdateAsync(
            Guid id,
            string code, int numeric, bool active, int order, string? name = null, string? symbol = null, string? country = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), CurrencyConsts.CodeMaxLength);
            Check.Length(name, nameof(name), CurrencyConsts.NameMaxLength);
            Check.Length(symbol, nameof(symbol), CurrencyConsts.SymbolMaxLength);
            Check.Length(country, nameof(country), CurrencyConsts.CountryMaxLength);

            var currency = await _currencyRepository.GetAsync(id);

            currency.Code = code;
            currency.Numeric = numeric;
            currency.Active = active;
            currency.Order = order;
            currency.Name = name;
            currency.Symbol = symbol;
            currency.Country = country;

            currency.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _currencyRepository.UpdateAsync(currency);
        }

    }
}