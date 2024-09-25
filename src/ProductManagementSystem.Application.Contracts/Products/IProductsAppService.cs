using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ProductManagementSystem.Shared;

namespace ProductManagementSystem.Products
{
    public partial interface IProductsAppService : IApplicationService
    {

        Task<PagedResultDto<ProductDto>> GetListAsync(GetProductsInput input);

        Task<ProductDto> GetAsync(long id);

        Task DeleteAsync(long id);

        Task<ProductDto> CreateAsync(ProductCreateDto input);

        Task<ProductDto> UpdateAsync(long id, ProductUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ProductExcelDownloadDto input);
        Task DeleteByIdsAsync(List<long> productIds);

        Task DeleteAllAsync(GetProductsInput input);
        Task<ProductManagementSystem.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();

    }
}