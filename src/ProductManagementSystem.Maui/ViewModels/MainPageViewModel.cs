using CommunityToolkit.Mvvm.Input;
using Volo.Abp.DependencyInjection;

namespace ProductManagementSystem.Maui.ViewModels;

public partial class MainPageViewModel : ProductManagementSystemViewModelBase, ITransientDependency
{
    public MainPageViewModel()
    {
    }

    [RelayCommand]
    async Task SeeAllUsers()
    {
        await Shell.Current.GoToAsync("///users");
    }
}
