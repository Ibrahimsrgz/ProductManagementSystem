using ProductManagementSystem.Maui.ViewModels;
using Volo.Abp.DependencyInjection;

namespace ProductManagementSystem.Maui.Pages;

public partial class TenantCreatePage : ContentPage, ITransientDependency
{
    public TenantCreatePage(TenantCreateViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}
