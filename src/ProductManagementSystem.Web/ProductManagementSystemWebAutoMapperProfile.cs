using ProductManagementSystem.Web.Pages.Currencies;
using ProductManagementSystem.Currencies;
using ProductManagementSystem.Web.Pages.Products;
using Volo.Abp.AutoMapper;
using ProductManagementSystem.Products;
using AutoMapper;

namespace ProductManagementSystem.Web;

public class ProductManagementSystemWebAutoMapperProfile : Profile
{
    public ProductManagementSystemWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.

        CreateMap<ProductDto, ProductUpdateViewModel>();
        CreateMap<ProductUpdateViewModel, ProductUpdateDto>();
        CreateMap<ProductCreateViewModel, ProductCreateDto>();

        CreateMap<CurrencyDto, CurrencyUpdateViewModel>();
        CreateMap<CurrencyUpdateViewModel, CurrencyUpdateDto>();
        CreateMap<CurrencyCreateViewModel, CurrencyCreateDto>();
    }
}