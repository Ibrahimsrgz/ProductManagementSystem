using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.MauiClient.Helpers
{

    /// <summary>
    /// Bu static class appsettings.json dosyasını okuyabilmek için yazılmıştır.
    /// var configuration = ConfigurationHelper.LoadConfiguration();
    /// string apiUrl = configuration["ApiUrl"];
    /// string databaseName = configuration.GetSection("DatabaseSettings:DatabaseName").Value;
    /// </summary>
    public static class ConfigurationHelper
    {
        public static IConfiguration LoadConfiguration()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ProductManagementSystem.MauiClient.appsettings.json";

            using var stream = assembly.GetManifestResourceStream(resourceName);

            if (stream == null)
            {
                throw new FileNotFoundException($"The configuration file '{resourceName}' was not found as an embedded resource.");
            }

            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();

            return config;
        }
    }
}
