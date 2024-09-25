using ProductManagementSystem.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using ProductManagementSystem.EntityFrameworkCore;

namespace ProductManagementSystem.Products
{
    public abstract class EfCoreProductRepositoryBase : EfCoreRepository<ProductManagementSystemDbContext, Product, long>
    {
        public EfCoreProductRepositoryBase(IDbContextProvider<ProductManagementSystemDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<ProductWithNavigationProperties> GetWithNavigationPropertiesAsync(long id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(product => new ProductWithNavigationProperties
                {
                    Product = product,
                    Currency = dbContext.Set<Currency>().FirstOrDefault(c => c.Id == product.CurrencyId)
                }).FirstOrDefault();
        }

        public virtual async Task<List<ProductWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? name = null,
            string? code = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            int? quantityMin = null,
            int? quantityMax = null,
            Guid? currencyId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, code, priceMin, priceMax, quantityMin, quantityMax, currencyId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ProductConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ProductWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from product in (await GetDbSetAsync())
                   join currency in (await GetDbContextAsync()).Set<Currency>() on product.CurrencyId equals currency.Id into currencies
                   from currency in currencies.DefaultIfEmpty()
                   select new ProductWithNavigationProperties
                   {
                       Product = product,
                       Currency = currency
                   };
        }

        protected virtual IQueryable<ProductWithNavigationProperties> ApplyFilter(
            IQueryable<ProductWithNavigationProperties> query,
            string? filterText,
            string? name = null,
            string? code = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            int? quantityMin = null,
            int? quantityMax = null,
            Guid? currencyId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Product.Name!.Contains(filterText!) || e.Product.Code!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Product.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Product.Code.Contains(code))
                    .WhereIf(priceMin.HasValue, e => e.Product.Price >= priceMin!.Value)
                    .WhereIf(priceMax.HasValue, e => e.Product.Price <= priceMax!.Value)
                    .WhereIf(quantityMin.HasValue, e => e.Product.Quantity >= quantityMin!.Value)
                    .WhereIf(quantityMax.HasValue, e => e.Product.Quantity <= quantityMax!.Value)
                    .WhereIf(currencyId != null && currencyId != Guid.Empty, e => e.Currency != null && e.Currency.Id == currencyId);
        }

        public virtual async Task<List<Product>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? code = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            int? quantityMin = null,
            int? quantityMax = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, code, priceMin, priceMax, quantityMin, quantityMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ProductConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? code = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            int? quantityMin = null,
            int? quantityMax = null,
            Guid? currencyId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, code, priceMin, priceMax, quantityMin, quantityMax, currencyId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Product> ApplyFilter(
            IQueryable<Product> query,
            string? filterText = null,
            string? name = null,
            string? code = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            int? quantityMin = null,
            int? quantityMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!) || e.Code!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(priceMin.HasValue, e => e.Price >= priceMin!.Value)
                    .WhereIf(priceMax.HasValue, e => e.Price <= priceMax!.Value)
                    .WhereIf(quantityMin.HasValue, e => e.Quantity >= quantityMin!.Value)
                    .WhereIf(quantityMax.HasValue, e => e.Quantity <= quantityMax!.Value);
        }
    }
}