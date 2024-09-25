using System;
using ProductManagementSystem.Shared;
using Volo.Abp.AutoMapper;
using ProductManagementSystem.Products;
using AutoMapper;

namespace ProductManagementSystem;

public class ProductManagementSystemApplicationAutoMapperProfile : Profile
{
    public ProductManagementSystemApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Product, ProductDto>();
        CreateMap<Product, ProductExcelDto>();
    }
}