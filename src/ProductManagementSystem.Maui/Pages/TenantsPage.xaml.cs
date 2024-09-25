using ProductManagementSystem.Maui.ViewModels;
using Volo.Abp.DependencyInjection;

namespace ProductManagementSystem.Maui.Pages;

public partial class TenantsPage : ContentPage, ITransientDependency
{
	public TenantsPage(TenantsPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
