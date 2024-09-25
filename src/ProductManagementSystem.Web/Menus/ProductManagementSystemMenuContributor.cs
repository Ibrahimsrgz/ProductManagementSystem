using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProductManagementSystem.Localization;
using ProductManagementSystem.Permissions;
using ProductManagementSystem.MultiTenancy;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.Account.Localization;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.Account.AuthorityDelegation;
using Volo.Abp.AuditLogging.Web.Navigation;
using Volo.Abp.LanguageManagement.Navigation;
using Volo.Abp.TextTemplateManagement.Web.Navigation;
using Volo.Abp.OpenIddict.Pro.Web.Menus;
using Volo.Saas.Host.Navigation;

namespace ProductManagementSystem.Web.Menus;

public class ProductManagementSystemMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public ProductManagementSystemMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<ProductManagementSystemResource>();
        //Home
        context.Menu.AddItem(
            new ApplicationMenuItem(
                ProductManagementSystemMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fa fa-home",
                order: 1
            )
        );

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 5;

        //Host Dashboard
        context.Menu.AddItem(
            new ApplicationMenuItem(
                ProductManagementSystemMenus.HostDashboard,
                l["Menu:Dashboard"],
                "~/HostDashboard",
                icon: "fa fa-line-chart",
                order: 2
            ).RequirePermissions(ProductManagementSystemPermissions.Dashboard.Host)
        );

        //Tenant Dashboard
        context.Menu.AddItem(
            new ApplicationMenuItem(
                ProductManagementSystemMenus.TenantDashboard,
                l["Menu:Dashboard"],
                "~/Dashboard",
                icon: "fa fa-line-chart",
                order: 2
            ).RequirePermissions(ProductManagementSystemPermissions.Dashboard.Tenant)
        );

        //Administration->Saas
        administration.SetSubItemOrder(SaasHostMenuNames.GroupName, 1);

        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);

        //Administration->OpenIddict
        administration.SetSubItemOrder(OpenIddictProMenus.GroupName, 3);

        //Administration->Language Management
        administration.SetSubItemOrder(LanguageManagementMenuNames.GroupName, 4);

        //Administration->Text Template Management
        administration.SetSubItemOrder(TextTemplateManagementMainMenuNames.GroupName, 5);

        //Administration->Audit Logs
        administration.SetSubItemOrder(AbpAuditLoggingMainMenuNames.GroupName, 6);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 7);

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ProductManagementSystemMenus.Products,
                l["Menu:Products"],
                url: "/Products",
icon: "fa fa-file-alt",
                requiredPermissionName: ProductManagementSystemPermissions.Products.Default)
        );
        return Task.CompletedTask;
    }

    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();
        var authorityDelegationOptions = context.ServiceProvider.GetRequiredService<IOptions<AbpAccountAuthorityDelegationOptions>>().Value;

        var uiResource = context.GetLocalizer<AbpUiResource>();
        var accountResource = context.GetLocalizer<AccountResource>();

        var authServerUrl = _configuration["AuthServer:Authority"] ?? "~";

        context.Menu.AddItem(new ApplicationMenuItem("Account.LinkedAccounts", accountResource["LinkedAccounts"], url: "javascript:void(0)", icon: "fa fa-link"));
        if (currentUser.FindImpersonatorUserId() == null && authorityDelegationOptions.EnableDelegatedImpersonation)
        {
            context.Menu.AddItem(new ApplicationMenuItem("Account.AuthorityDelegation", accountResource["AuthorityDelegation"], url: "javascript:void(0)", icon: "fa fa-users"));
        }

        context.Menu.AddItem(new ApplicationMenuItem("Account.Manage", accountResource["MyAccount"], $"{authServerUrl.EnsureEndsWith('/')}Account/Manage", icon: "fa fa-cog", order: 1000, target: "_blank").RequireAuthenticated());
        context.Menu.AddItem(new ApplicationMenuItem("Account.SecurityLogs", accountResource["MySecurityLogs"], $"{authServerUrl.EnsureEndsWith('/')}Account/SecurityLogs", icon: "fa fa-user-shield", target: "_blank").RequireAuthenticated());
        context.Menu.AddItem(new ApplicationMenuItem("Account.Sessions", accountResource["Sessions"], url: $"{authServerUrl.EnsureEndsWith('/')}Account/Sessions", icon: "fa fa-clock", target: "_blank").RequireAuthenticated());
        context.Menu.AddItem(new ApplicationMenuItem("Account.Logout", uiResource["Logout"], url: "~/Account/Logout", icon: "fa fa-power-off", order: int.MaxValue - 1000).RequireAuthenticated());

        return Task.CompletedTask;
    }
}