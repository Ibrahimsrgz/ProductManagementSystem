using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ProductManagementSystem.Currencies
{
    public partial interface ICurrencyRepository : IRepository<Currency, Guid>
    {
        Task<List<Currency>> GetListAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);
    }
}