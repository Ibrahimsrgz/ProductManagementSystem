using ProductManagementSystem.MauiClient.Models;

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
