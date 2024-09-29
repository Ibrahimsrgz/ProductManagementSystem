using ProductManagementSystem.MauiClient.Database;
using ProductManagementSystem.MauiClient.Models;
using ProductManagementSystem.MauiClient.ViewModels;
using System.ComponentModel;

namespace ProductManagementSystem.MauiClient.Pages
{
    public partial class ProductListPage : ContentPage, INotifyPropertyChanged
    {
        private readonly IProductListViewModel _viewModel;

        public ProductListPage(AppDbContext dbContext, IProductListViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        //viewmodelin load datası constructor üzerinden async yapamadığımız için çalışmıyor.
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _viewModel.InitializeAsync();
        }

        private async void OnItemTapped(object sender, EventArgs e)
        {
            var label = sender as Label;
            var selectedProduct = label?.BindingContext as ProductItem;

            _viewModel.Items.Clear(); 

            if (selectedProduct != null)
            {
                await Navigation.PushAsync(new ProductDetailPage(selectedProduct));
            }
        }
    }
}
