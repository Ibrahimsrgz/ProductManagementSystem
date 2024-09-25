using ProductManagementSystem.Shared;
using ProductManagementSystem.Currencies;
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
using ProductManagementSystem.Products;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ProductManagementSystem.Shared;

namespace ProductManagementSystem.Products
{
    [RemoteService(IsEnabled = false)]
    [Authorize(ProductManagementSystemPermissions.Products.Default)]
    public abstract class ProductsAppServiceBase : ProductManagementSystemAppService
    {
        protected IDistributedCache<ProductDownloadTokenCacheItem, string> _downloadTokenCache;
        protected IProductRepository _productRepository;
        protected ProductManager _productManager;

        protected IRepository<ProductManagementSystem.Currencies.Currency, Guid> _currencyRepository;

        public ProductsAppServiceBase(IProductRepository productRepository, ProductManager productManager, IDistributedCache<ProductDownloadTokenCacheItem, string> downloadTokenCache, IRepository<ProductManagementSystem.Currencies.Currency, Guid> currencyRepository)
        {
            _downloadTokenCache = downloadTokenCache;
            _productRepository = productRepository;
            _productManager = productManager; _currencyRepository = currencyRepository;

        }

        [AllowAnonymous]
        public virtual async Task<PagedResultDto<ProductWithNavigationPropertiesDto>> GetListAsync(GetProductsInput input)
        {
            var totalCount = await _productRepository.GetCountAsync(input.FilterText, input.Name, input.Code, input.PriceMin, input.PriceMax, input.QuantityMin, input.QuantityMax, input.CurrencyId);
            var items = await _productRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Name, input.Code, input.PriceMin, input.PriceMax, input.QuantityMin, input.QuantityMax, input.CurrencyId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ProductWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ProductWithNavigationProperties>, List<ProductWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ProductWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(long id)
        {
            return ObjectMapper.Map<ProductWithNavigationProperties, ProductWithNavigationPropertiesDto>
                (await _productRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<ProductDto> GetAsync(long id)
        {
            return ObjectMapper.Map<Product, ProductDto>(await _productRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCurrencyLookupAsync(LookupRequestDto input)
        {
            var query = (await _currencyRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<ProductManagementSystem.Currencies.Currency>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ProductManagementSystem.Currencies.Currency>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(ProductManagementSystemPermissions.Products.Delete)]
        public virtual async Task DeleteAsync(long id)
        {
            await _productRepository.DeleteAsync(id);
        }

        [Authorize(ProductManagementSystemPermissions.Products.Create)]
        public virtual async Task<ProductDto> CreateAsync(ProductCreateDto input)
        {
            if (input.CurrencyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Currency"]]);
            }

            var product = await _productManager.CreateAsync(
            input.CurrencyId, input.Name, input.Code, input.Price, input.Quantity
            );

            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        [Authorize(ProductManagementSystemPermissions.Products.Edit)]
        public virtual async Task<ProductDto> UpdateAsync(long id, ProductUpdateDto input)
        {
            if (input.CurrencyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Currency"]]);
            }

            var product = await _productManager.UpdateAsync(
            id,
            input.CurrencyId, input.Name, input.Code, input.Price, input.Quantity, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ProductExcelDownloadDto input)
        {
            var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var products = await _productRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Name, input.Code, input.PriceMin, input.PriceMax, input.QuantityMin, input.QuantityMax, input.CurrencyId);
            var items = products.Select(item => new
            {
                Name = item.Product.Name,
                Code = item.Product.Code,
                Price = item.Product.Price,
                Quantity = item.Product.Quantity,

                Currency = item.Currency?.Name,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Products.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<ProductManagementSystem.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _downloadTokenCache.SetAsync(
                token,
                new ProductDownloadTokenCacheItem { Token = token },
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