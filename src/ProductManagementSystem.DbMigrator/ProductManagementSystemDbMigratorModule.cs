using ProductManagementSystem.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace ProductManagementSystem.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(ProductManagementSystemEntityFrameworkCoreModule),
    typeof(ProductManagementSystemApplicationContractsModule)
)]
public class ProductManagementSystemDbMigratorModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        if (Program.DisableRedis)
        {
            var configuration = context.Services.GetConfiguration();
            configuration["Redis:IsEnabled"] = "false";
        }
        
        base.PreConfigureServices(context);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "ProductManagementSystem:"; });
    }
}
