using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductManagementSystem.MauiClient.Database;
using ProductManagementSystem.MauiClient.Dtos.Product;
using ProductManagementSystem.MauiClient.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProductManagementSystem.MauiClient.ViewModels
{
    public class ProductListViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _dbContext;
        private readonly HttpClient _httpClient = new HttpClient();

        public ObservableCollection<ProductItem> Items { get; set; } = new ObservableCollection<ProductItem>();

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; 
                OnPropertyChanged(nameof(IsLoading));
                OnPropertyChanged(nameof(IsNotLoadingAndNotError));
            }
        }

        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set { _isError = value; 
                OnPropertyChanged(nameof(IsLoading));
                OnPropertyChanged(nameof(IsNotLoadingAndNotError));
            }
        }

        public bool IsNotLoadingAndNotError => !IsLoading && !IsError;

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

      
        public ProductListViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InitializeAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            IsLoading = true;
            IsError = false;

            try
            {
                var configuration = Helpers.ConfigurationHelper.LoadConfiguration();
                string apiUrl = configuration.GetSection("Urls:ApiUrl").Value;

                string url = apiUrl + "/api/app/products";
                
                var response = await _httpClient.GetStringAsync(url);
                
                var items = JsonConvert.DeserializeObject<GetProductItemsResponseDto>(response);

                _dbContext.Products.RemoveRange(_dbContext.Products);
                await _dbContext.SaveChangesAsync();


                if (items != null)
                {
                    foreach (var item in items.Items)
                    {
                        var productItem = new ProductItem
                        {
                            Name = item.Product.Name,
                            Code = item.Product.Code,
                            Price = item.Product.Price,
                            Quantity = item.Product.Quantity,
                            CurrencySymbol = item.Currency.Symbol
                        };

                        await _dbContext.Products.AddAsync(productItem);
                    }

                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    IsError = true;
                    ErrorMessage = "No data returned from server.";
                }
                Items.Clear();

                // Veritabanından verileri çek ve CollectionView'i güncelle
                var products = await _dbContext.Products.ToListAsync();
                foreach (var product in products)
                {
                    Items.Add(product);
                }
            }
            catch (HttpRequestException httpEx)
            {
                IsError = true;
                ErrorMessage = $"HTTP Error: {httpEx.Message}";
            }
            catch (Exception ex)
            {
                IsError = true;
                ErrorMessage = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
