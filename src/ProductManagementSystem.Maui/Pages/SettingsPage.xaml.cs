using ProductManagementSystem.Maui.ViewModels;
using Volo.Abp.DependencyInjection;

namespace ProductManagementSystem.Maui.Pages;

public partial class SettingsPage : ContentPage, ITransientDependency
{
	public SettingsPage(SettingsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}