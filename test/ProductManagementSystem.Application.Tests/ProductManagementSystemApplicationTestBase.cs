using Volo.Abp.Modularity;

namespace ProductManagementSystem;

public abstract class ProductManagementSystemApplicationTestBase<TStartupModule> : ProductManagementSystemTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
