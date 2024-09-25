using Volo.Abp.Modularity;

namespace ProductManagementSystem;

/* Inherit from this class for your domain layer tests. */
public abstract class ProductManagementSystemDomainTestBase<TStartupModule> : ProductManagementSystemTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
