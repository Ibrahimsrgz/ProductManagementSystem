using ProductManagementSystem.MauiClient.Database;
namespace ProductManagementSystem.MauiClient;

public partial class App : Application
{
	public App(MainPage mainPage)
	{
		InitializeComponent();

		//MainPage = new AppShell();
        MainPage = new NavigationPage(mainPage); // Ensure MainPage is resolved through DI
    }
}
