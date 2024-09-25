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

        public virtual async Task DeleteAllAsync(
            string? filterText = null,
                        string? name = null,
            string? code = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            int? quantityMin = null,
            int? quantityMax = null,
            CancellationToken cancellationToken = default)
        {

            var query = await GetQueryableAsync();

            query = ApplyFilter(query, filterText, name, code, priceMin, priceMax, quantityMin, quantityMax);

            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, code, priceMin, priceMax, quantityMin, quantityMax);
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