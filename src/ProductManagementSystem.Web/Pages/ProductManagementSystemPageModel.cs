using ProductManagementSystem.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace ProductManagementSystem.Web.Pages;

/* Inherit your Page Model classes from this class.
 */
public abstract class ProductManagementSystemPageModel : AbpPageModel
{
    protected ProductManagementSystemPageModel()
    {
        LocalizationResourceType = typeof(ProductManagementSystemResource);
    }
}
