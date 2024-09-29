using ProductManagementSystem.MauiClient.Dtos.Product;
using ProductManagementSystem.MauiClient.ViewModels;

namespace ProductManagementSystem.MauiClient.Pages
{
    public partial class ProductDetailPage : ContentPage
    {
        public ProductDetailPage(ProductItem selectedItem)
        {
            InitializeComponent();
            BindingContext = selectedItem; // Veriyi sayfaya bağla
        }
    }
}
