using ProductManagementSystem.Maui.ViewModels;
using Volo.Abp.DependencyInjection;

namespace ProductManagementSystem.Maui.Pages;

public partial class ProfilePicturePage : ContentPage, ITransientDependency
{
    public ProfilePicturePage(ProfilePictureViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}