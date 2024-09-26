using ProductManagementSystem.MauiClient.Pages;

namespace ProductManagementSystem.MauiClient;

public partial class MainPage : ContentPage
{
	

	public MainPage()
	{
		InitializeComponent();
	}
    private async void OnGoToSecondPageClicked(object sender, EventArgs e)
    {
        // Sayfaya yönlendirme
        await Navigation.PushAsync(new ProductListPage());
    }
  


}

