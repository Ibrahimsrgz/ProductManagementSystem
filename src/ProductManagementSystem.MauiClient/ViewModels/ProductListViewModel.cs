using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductManagementSystem.MauiClient.Database;
using ProductManagementSystem.MauiClient.Dtos.Product;
using ProductManagementSystem.MauiClient.Models;
using ProductManagementSystem.MauiClient.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProductManagementSystem.MauiClient.ViewModels
{
    public interface IProductListViewModel
    {
        ObservableCollection<ProductItem> Items { get; set; }
        Task InitializeAsync();
    }

    public class ProductListViewModel : BaseViewModel, IProductListViewModel
    {
        private readonly AppDbContext _dbContext;
        private readonly IProductService _productService;

        public ObservableCollection<ProductItem> Items { get; set; } = new ObservableCollection<ProductItem>();

        public ProductListViewModel(AppDbContext dbContext, IProductService productService)
        {
            _dbContext = dbContext;
            _productService = productService;
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
                var items = await _productService.GetProductsAsync();

                // Clear existing products in the database
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

                // Retrieve data from the database and update the CollectionView
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
    }
}
