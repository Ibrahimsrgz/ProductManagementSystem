using Volo.Abp.Modularity;

namespace ProductManagementSystem;

[DependsOn(
    typeof(ProductManagementSystemApplicationModule),
    typeof(ProductManagementSystemDomainTestModule)
)]
public class ProductManagementSystemApplicationTestModule : AbpModule
{

}
