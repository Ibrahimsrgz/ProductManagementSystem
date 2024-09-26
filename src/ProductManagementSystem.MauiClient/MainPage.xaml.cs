using ProductManagementSystem.MauiClient.Pages;

namespace ProductManagementSystem.MauiClient;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}
    private async void OnGoToSecondPageClicked(object sender, EventArgs e)
    {
        // Sayfaya yönlendirme
        await Navigation.PushAsync(new ProductListPage());
    }
    //private void OnCounterClicked(object sender, EventArgs e)
    //{
    //	count++;

    //	if (count == 1)
    //		CounterBtn.Text = $"Clicked {count} time";
    //	else
    //		CounterBtn.Text = $"Clicked {count} times";

    //	SemanticScreenReader.Announce(CounterBtn.Text);
    //}
}

