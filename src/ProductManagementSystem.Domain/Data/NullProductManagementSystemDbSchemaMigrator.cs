using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ProductManagementSystem.Data;

/* This is used if database provider does't define
 * IProductManagementSystemDbSchemaMigrator implementation.
 */
public class NullProductManagementSystemDbSchemaMigrator : IProductManagementSystemDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
