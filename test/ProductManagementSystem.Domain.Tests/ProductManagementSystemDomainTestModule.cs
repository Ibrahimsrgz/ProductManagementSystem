using Volo.Abp.Modularity;

namespace ProductManagementSystem;

[DependsOn(
    typeof(ProductManagementSystemDomainModule),
    typeof(ProductManagementSystemTestBaseModule)
)]
public class ProductManagementSystemDomainTestModule : AbpModule
{

}
