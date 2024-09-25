using ProductManagementSystem.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace ProductManagementSystem.Permissions;

public class ProductManagementSystemPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ProductManagementSystemPermissions.GroupName);

        myGroup.AddPermission(ProductManagementSystemPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(ProductManagementSystemPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(ProductManagementSystemPermissions.MyPermission1, L("Permission:MyPermission1"));

        var productPermission = myGroup.AddPermission(ProductManagementSystemPermissions.Products.Default, L("Permission:Products"));
        productPermission.AddChild(ProductManagementSystemPermissions.Products.Create, L("Permission:Create"));
        productPermission.AddChild(ProductManagementSystemPermissions.Products.Edit, L("Permission:Edit"));
        productPermission.AddChild(ProductManagementSystemPermissions.Products.Delete, L("Permission:Delete"));

        var currencyPermission = myGroup.AddPermission(ProductManagementSystemPermissions.Currencies.Default, L("Permission:Currencies"));
        currencyPermission.AddChild(ProductManagementSystemPermissions.Currencies.Create, L("Permission:Create"));
        currencyPermission.AddChild(ProductManagementSystemPermissions.Currencies.Edit, L("Permission:Edit"));
        currencyPermission.AddChild(ProductManagementSystemPermissions.Currencies.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ProductManagementSystemResource>(name);
    }
}