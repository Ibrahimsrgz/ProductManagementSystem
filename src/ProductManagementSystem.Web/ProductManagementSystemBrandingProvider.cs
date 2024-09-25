using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using ProductManagementSystem.Localization;

namespace ProductManagementSystem.Web;

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
