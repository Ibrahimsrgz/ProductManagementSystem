using ProductManagementSystem.Localization;
using Volo.Abp.Application.Services;

namespace ProductManagementSystem;

/* Inherit your application services from this class.
 */
public abstract class ProductManagementSystemAppService : ApplicationService
{
    protected ProductManagementSystemAppService()
    {
        LocalizationResource = typeof(ProductManagementSystemResource);
    }
}
