using ProductManagementSystem.Shared;
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

        Task<PagedResultDto<ProductWithNavigationPropertiesDto>> GetListAsync(GetProductsInput input);

        Task<ProductWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(long id);

        Task<ProductDto> GetAsync(long id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCurrencyLookupAsync(LookupRequestDto input);

        Task DeleteAsync(long id);

        Task<ProductDto> CreateAsync(ProductCreateDto input);

        Task<ProductDto> UpdateAsync(long id, ProductUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ProductExcelDownloadDto input);

        Task<ProductManagementSystem.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();

    }
}