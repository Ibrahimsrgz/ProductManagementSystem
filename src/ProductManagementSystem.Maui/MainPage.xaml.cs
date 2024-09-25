using ProductManagementSystem.Maui.ViewModels;
using Volo.Abp.DependencyInjection;

namespace ProductManagementSystem.Maui;

public partial class MainPage : ContentPage, ITransientDependency
{
    public MainPage(
		MainPageViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
    }
}