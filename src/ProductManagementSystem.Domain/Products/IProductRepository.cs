using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ProductManagementSystem.Products
{
    public partial interface IProductRepository : IRepository<Product, long>
    {
        Task<ProductWithNavigationProperties> GetWithNavigationPropertiesAsync(
    long id,
    CancellationToken cancellationToken = default
);

        Task<List<ProductWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<List<Product>> GetListAsync(
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
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? code = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            int? quantityMin = null,
            int? quantityMax = null,
            Guid? currencyId = null,
            CancellationToken cancellationToken = default);
    }
}