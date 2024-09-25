using ProductManagementSystem.Maui.ViewModels;
using Volo.Abp.DependencyInjection;

namespace ProductManagementSystem.Maui.Pages;

public partial class TenantEditPage : ContentPage, ITransientDependency
{
	public TenantEditPage(TenantEditViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}
