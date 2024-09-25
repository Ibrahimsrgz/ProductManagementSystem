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

        public ProductsAppServiceBase(IProductRepository productRepository, ProductManager productManager, IDistributedCache<ProductDownloadTokenCacheItem, string> downloadTokenCache)
        {
            _downloadTokenCache = downloadTokenCache;
            _productRepository = productRepository;
            _productManager = productManager;

        }

        public virtual async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductsInput input)
        {
            var totalCount = await _productRepository.GetCountAsync(input.FilterText, input.Name, input.Code, input.PriceMin, input.PriceMax, input.QuantityMin, input.QuantityMax);
            var items = await _productRepository.GetListAsync(input.FilterText, input.Name, input.Code, input.PriceMin, input.PriceMax, input.QuantityMin, input.QuantityMax, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ProductDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Product>, List<ProductDto>>(items)
            };
        }

        public virtual async Task<ProductDto> GetAsync(long id)
        {
            return ObjectMapper.Map<Product, ProductDto>(await _productRepository.GetAsync(id));
        }

        [Authorize(ProductManagementSystemPermissions.Products.Delete)]
        public virtual async Task DeleteAsync(long id)
        {
            await _productRepository.DeleteAsync(id);
        }

        [Authorize(ProductManagementSystemPermissions.Products.Create)]
        public virtual async Task<ProductDto> CreateAsync(ProductCreateDto input)
        {

            var product = await _productManager.CreateAsync(
            input.Name, input.Code, input.Price, input.Quantity
            );

            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        [Authorize(ProductManagementSystemPermissions.Products.Edit)]
        public virtual async Task<ProductDto> UpdateAsync(long id, ProductUpdateDto input)
        {

            var product = await _productManager.UpdateAsync(
            id,
            input.Name, input.Code, input.Price, input.Quantity, input.ConcurrencyStamp
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

            var items = await _productRepository.GetListAsync(input.FilterText, input.Name, input.Code, input.PriceMin, input.PriceMax, input.QuantityMin, input.QuantityMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Product>, List<ProductExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Products.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [Authorize(ProductManagementSystemPermissions.Products.Delete)]
        public virtual async Task DeleteByIdsAsync(List<long> productIds)
        {
            await _productRepository.DeleteManyAsync(productIds);
        }

        [Authorize(ProductManagementSystemPermissions.Products.Delete)]
        public virtual async Task DeleteAllAsync(GetProductsInput input)
        {
            await _productRepository.DeleteAllAsync(input.FilterText, input.Name, input.Code, input.PriceMin, input.PriceMax, input.QuantityMin, input.QuantityMax);
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