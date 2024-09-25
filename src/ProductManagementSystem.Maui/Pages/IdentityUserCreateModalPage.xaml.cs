using ProductManagementSystem.Maui.ViewModels;
using Volo.Abp.DependencyInjection;

namespace ProductManagementSystem.Maui.Pages;

public partial class IdentityUserCreateModalPage : ContentPage, ITransientDependency
{
    public IdentityUserCreateModalPage(IdentityUserCreateViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}