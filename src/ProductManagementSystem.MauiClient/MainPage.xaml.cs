using ProductManagementSystem.MauiClient.Pages;

namespace ProductManagementSystem.MauiClient;

public partial class MainPage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;

    // Modify the constructor to accept IServiceProvider
    public MainPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;  // Store the service provider for later use
    }
    private async void OnGetProductListPageClicked(object sender, EventArgs e)
    {
        // Resolve ProductListPage from the service provider
        var productListPage = _serviceProvider.GetRequiredService<ProductListPage>();

        // Navigate to ProductListPage
        await Navigation.PushAsync(productListPage);
    }
}


