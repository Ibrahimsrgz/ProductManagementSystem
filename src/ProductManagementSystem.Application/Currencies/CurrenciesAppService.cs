using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using ProductManagementSystem.Permissions;
using ProductManagementSystem.Currencies;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ProductManagementSystem.Shared;

namespace ProductManagementSystem.Currencies
{
    [RemoteService(IsEnabled = false)]
    [Authorize(ProductManagementSystemPermissions.Currencies.Default)]
    public abstract class CurrenciesAppServiceBase : ProductManagementSystemAppService
    {
        protected IDistributedCache<CurrencyDownloadTokenCacheItem, string> _downloadTokenCache;
        protected ICurrencyRepository _currencyRepository;
        protected CurrencyManager _currencyManager;

        public CurrenciesAppServiceBase(ICurrencyRepository currencyRepository, CurrencyManager currencyManager, IDistributedCache<CurrencyDownloadTokenCacheItem, string> downloadTokenCache)
        {
            _downloadTokenCache = downloadTokenCache;
            _currencyRepository = currencyRepository;
            _currencyManager = currencyManager;

        }

        public virtual async Task<PagedResultDto<CurrencyDto>> GetListAsync(GetCurrenciesInput input)
        {
            var totalCount = await _currencyRepository.GetCountAsync(input.FilterText, input.Code, input.NumericMin, input.NumericMax, input.Name, input.Symbol, input.Country, input.Active, input.OrderMin, input.OrderMax);
            var items = await _currencyRepository.GetListAsync(input.FilterText, input.Code, input.NumericMin, input.NumericMax, input.Name, input.Symbol, input.Country, input.Active, input.OrderMin, input.OrderMax, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CurrencyDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Currency>, List<CurrencyDto>>(items)
            };
        }

        public virtual async Task<CurrencyDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Currency, CurrencyDto>(await _currencyRepository.GetAsync(id));
        }

        [Authorize(ProductManagementSystemPermissions.Currencies.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _currencyRepository.DeleteAsync(id);
        }

        [Authorize(ProductManagementSystemPermissions.Currencies.Create)]
        public virtual async Task<CurrencyDto> CreateAsync(CurrencyCreateDto input)
        {

            var currency = await _currencyManager.CreateAsync(
            input.Code, input.Numeric, input.Active, input.Order, input.Name, input.Symbol, input.Country
            );

            return ObjectMapper.Map<Currency, CurrencyDto>(currency);
        }

        [Authorize(ProductManagementSystemPermissions.Currencies.Edit)]
        public virtual async Task<CurrencyDto> UpdateAsync(Guid id, CurrencyUpdateDto input)
        {

            var currency = await _currencyManager.UpdateAsync(
            id,
            input.Code, input.Numeric, input.Active, input.Order, input.Name, input.Symbol, input.Country, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Currency, CurrencyDto>(currency);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CurrencyExcelDownloadDto input)
        {
            var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _currencyRepository.GetListAsync(input.FilterText, input.Code, input.NumericMin, input.NumericMax, input.Name, input.Symbol, input.Country, input.Active, input.OrderMin, input.OrderMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Currency>, List<CurrencyExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Currencies.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<ProductManagementSystem.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _downloadTokenCache.SetAsync(
                token,
                new CurrencyDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new ProductManagementSystem.Shared.DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}