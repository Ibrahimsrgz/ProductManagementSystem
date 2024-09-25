using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ProductManagementSystem.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class ProductManagementSystemDbContextFactory : IDesignTimeDbContextFactory<ProductManagementSystemDbContext>
{
    public ProductManagementSystemDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        ProductManagementSystemEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<ProductManagementSystemDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new ProductManagementSystemDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ProductManagementSystem.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
