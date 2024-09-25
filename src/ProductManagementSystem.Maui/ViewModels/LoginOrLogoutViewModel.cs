using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProductManagementSystem.Maui.Messages;
using ProductManagementSystem.Maui.Oidc;
using Volo.Abp.DependencyInjection;

namespace ProductManagementSystem.Maui.ViewModels;

public partial class LoginOrLogoutViewModel : ProductManagementSystemViewModelBase, ITransientDependency
{
    protected ILoginService LoginService { get; }

    public LoginOrLogoutViewModel(ILoginService loginService)
    {
        LoginService = loginService;
    }

    [RelayCommand]
    async Task LoginOrLogout()
    {
        if (!CurrentUser.IsAuthenticated)
        {
            var loginResult = await LoginService.LoginAsync();

            if (loginResult.IsError)
            {
                await Shell.Current.DisplayAlert(L["Error"], loginResult.Error, L["Close"]);
            }
        }
        else
        {
            var logoutResult = await LoginService.LogoutAsync();

            if (logoutResult.IsError)
            {
                await Shell.Current.DisplayAlert(L["Error"], logoutResult.Error, L["Close"]);
            }
        }

        await Shell.Current.GoToAsync("///home");
    }
}
