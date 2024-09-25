using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductManagementSystem.Data;
using Volo.Abp.DependencyInjection;

namespace ProductManagementSystem.EntityFrameworkCore;

public class EntityFrameworkCoreProductManagementSystemDbSchemaMigrator
    : IProductManagementSystemDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreProductManagementSystemDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the ProductManagementSystemDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ProductManagementSystemDbContext>()
            .Database
            .MigrateAsync();
    }
}
