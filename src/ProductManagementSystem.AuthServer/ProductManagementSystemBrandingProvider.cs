using Microsoft.Extensions.Localization;
using ProductManagementSystem.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ProductManagementSystem;

[Dependency(ReplaceServices = true)]
public class ProductManagementSystemBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<ProductManagementSystemResource> _localizer;

    public ProductManagementSystemBrandingProvider(IStringLocalizer<ProductManagementSystemResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
