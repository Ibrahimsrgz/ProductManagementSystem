using ProductManagementSystem.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ProductManagementSystem.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ProductManagementSystemController : AbpControllerBase
{
    protected ProductManagementSystemController()
    {
        LocalizationResource = typeof(ProductManagementSystemResource);
    }
}
