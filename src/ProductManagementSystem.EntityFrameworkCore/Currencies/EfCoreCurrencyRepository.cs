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

namespace ProductManagementSystem.Currencies
{
    public abstract class EfCoreCurrencyRepositoryBase : EfCoreRepository<ProductManagementSystemDbContext, Currency, Guid>
    {
        public EfCoreCurrencyRepositoryBase(IDbContextProvider<ProductManagementSystemDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<List<Currency>> GetListAsync(
            string? filterText = null,
            string? code = null,
            int? numericMin = null,
            int? numericMax = null,
            string? name = null,
            string? symbol = null,
            string? country = null,
            bool? active = null,
            int? orderMin = null,
            int? orderMax = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, numericMin, numericMax, name, symbol, country, active, orderMin, orderMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CurrencyConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? code = null,
            int? numericMin = null,
            int? numericMax = null,
            string? name = null,
            string? symbol = null,
            string? country = null,
            bool? active = null,
            int? orderMin = null,
            int? orderMax = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, code, numericMin, numericMax, name, symbol, country, active, orderMin, orderMax);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Currency> ApplyFilter(
            IQueryable<Currency> query,
            string? filterText = null,
            string? code = null,
            int? numericMin = null,
            int? numericMax = null,
            string? name = null,
            string? symbol = null,
            string? country = null,
            bool? active = null,
            int? orderMin = null,
            int? orderMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code!.Contains(filterText!) || e.Name!.Contains(filterText!) || e.Symbol!.Contains(filterText!) || e.Country!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(numericMin.HasValue, e => e.Numeric >= numericMin!.Value)
                    .WhereIf(numericMax.HasValue, e => e.Numeric <= numericMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(symbol), e => e.Symbol.Contains(symbol))
                    .WhereIf(!string.IsNullOrWhiteSpace(country), e => e.Country.Contains(country))
                    .WhereIf(active.HasValue, e => e.Active == active)
                    .WhereIf(orderMin.HasValue, e => e.Order >= orderMin!.Value)
                    .WhereIf(orderMax.HasValue, e => e.Order <= orderMax!.Value);
        }
    }
}