using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.MauiClient.Database;
using ProductManagementSystem.MauiClient.Dtos.Product;
using ProductManagementSystem.MauiClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.MauiClient.ViewModels
{
    public class ProductListViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _dbContext;

        public ObservableCollection<ProductItem> Items { get; set; } = new ObservableCollection<ProductItem>();

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set { _isError = value; OnPropertyChanged(); }
        }

        public bool IsNotLoadingAndNotError => !IsLoading && !IsError;

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public ProductListViewModel()
        {
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
                //string url = "https://1707-212-108-134-135.ngrok-free.app/api/app/products";
                //var response = await _httpClient.GetStringAsync(url);

                //var items = JsonConvert.DeserializeObject<GetProductItemsResponseDto>(response);


                // Mock verileri oluşturuyoruz
                var mockItems = new GetProductItemsResponseDto
                {
                    Items = new List<ProductItemDto>
            {
                new ProductItemDto
                {
                    Product = new ProductDto { Name = "Laptop", Code = "LP1001", Price = 999.99m, Quantity = 10 },
                    Currency = new CurrencyDto { Symbol = "$$" }
                },
                new ProductItemDto
                {
                    Product = new ProductDto { Name = "Mouse", Code = "MS2002", Price = 25.50m, Quantity = 50 },
                    Currency = new CurrencyDto { Symbol = "$$" }
                },
                new ProductItemDto
                {
                    Product = new ProductDto { Name = "Keyboard", Code = "KB3003", Price = 45.99m, Quantity = 30 },
                    Currency = new CurrencyDto { Symbol = "$$" }
                },
                new ProductItemDto
                {
                    Product = new ProductDto { Name = "Monitor", Code = "MN4004", Price = 199.99m, Quantity = 20 },
                    Currency = new CurrencyDto { Symbol = "$$" }
                },
                new ProductItemDto
                {
                    Product = new ProductDto { Name = "Printer", Code = "PR5005", Price = 150.00m, Quantity = 15 },
                    Currency = new CurrencyDto { Symbol = "$$" }
                },
                new ProductItemDto
                {
                    Product = new ProductDto { Name = "Tablet", Code = "TB6006", Price = 299.99m, Quantity = 25 },
                    Currency = new CurrencyDto { Symbol = "$$" }
                },
                new ProductItemDto
                {
                    Product = new ProductDto { Name = "Smartphone", Code = "SP7007", Price = 799.99m, Quantity = 60 },
                    Currency = new CurrencyDto { Symbol = "$$" }
                },
                new ProductItemDto
                {
                    Product = new ProductDto { Name = "Smartwatch", Code = "SW8008", Price = 199.99m, Quantity = 40 },
                    Currency = new CurrencyDto { Symbol = "$$" }
                },
                new ProductItemDto
                {
                    Product = new ProductDto { Name = "Camera", Code = "CM9009", Price = 450.00m, Quantity = 12 },
                    Currency = new CurrencyDto { Symbol = "$$" }
                },
                new ProductItemDto
                {
                    Product = new ProductDto { Name = "Headphones", Code = "HP1010", Price = 79.99m, Quantity = 100 },
                    Currency = new CurrencyDto { Symbol = "$$" }
                }
                },
                    TotalCount = 10
                };




                //if (items != null)
                //{
                //    foreach (var item in items.Items)
                //    {
                //        Items.Add(item);
                //    }
                //}
                //else
                //{
                //    IsError = true;
                //    ErrorMessage = "No data returned from server.";
                //}
                _dbContext.Products.RemoveRange(_dbContext.Products);
                await _dbContext.SaveChangesAsync();


                foreach (var item in mockItems.Items)
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

                // Veritabanından verileri çek ve CollectionView'i güncelle
                Items.Clear();

                await Task.Delay(3000);


                var products = await _dbContext.Products.ToListAsync();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Items.Clear();
                    foreach (var product in products)
                    {
                        Items.Add(product);
                    }
                });

                //foreach (var product in products)
                //{
                //    Items.Add(product);
                //}
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
