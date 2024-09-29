using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProductManagementSystem.MauiClient.Database;
using ProductManagementSystem.MauiClient.Pages;
using ProductManagementSystem.MauiClient.Services;
using ProductManagementSystem.MauiClient.ViewModels;
using System.Reflection;

namespace ProductManagementSystem.MauiClient;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        var configuration =Helpers.ConfigurationHelper.LoadConfiguration();

        // Veritabanı dosya yolunu yapılandırmadan alın
        string dbName = configuration.GetSection("DatabaseSettings:DatabaseName").Value;

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, dbName);

        // Register AppDbContext for DI
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite($"Filename={dbPath}");
        }, ServiceLifetime.Transient);

        // Register ProductListPage for DI
        builder.Services.AddTransient<ProductListPage>();

        // Detail page her defasında yeni bir instance oluşturur
        builder.Services.AddTransient<ProductDetailPage>();

        builder.Services.AddSingleton<HttpClient>();
        builder.Services.AddTransient<IProductService, ProductService>();
        //builder.Services.AddTransient<ProductListViewModel>();
        builder.Services.AddTransient<IProductListViewModel, ProductListViewModel>();

        // Register MainPage for DI and pass IServiceProvider
        builder.Services.AddSingleton<MainPage>();

        var app = builder.Build();

        // Veritabanını oluştur
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.EnsureCreated();
        }

        return app;
    }
}
