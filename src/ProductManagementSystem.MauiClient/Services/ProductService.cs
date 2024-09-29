using Newtonsoft.Json;
using ProductManagementSystem.MauiClient.Dtos.Product;

namespace ProductManagementSystem.MauiClient.Services
{
    public interface IProductService
    {
        Task<GetProductItemsResponseDto> GetProductsAsync();
    }

    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetProductItemsResponseDto> GetProductsAsync()
        {
            var configuration = Helpers.ConfigurationHelper.LoadConfiguration();
            string apiUrl = configuration.GetSection("Urls:ApiUrl").Value;
            string url = $"{apiUrl}/api/app/products";

            var response = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<GetProductItemsResponseDto>(response);
        }
    }
}
