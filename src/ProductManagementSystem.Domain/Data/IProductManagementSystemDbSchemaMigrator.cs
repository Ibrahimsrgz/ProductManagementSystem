using System.Threading.Tasks;

namespace ProductManagementSystem.Data;

public interface IProductManagementSystemDbSchemaMigrator
{
    Task MigrateAsync();
}
