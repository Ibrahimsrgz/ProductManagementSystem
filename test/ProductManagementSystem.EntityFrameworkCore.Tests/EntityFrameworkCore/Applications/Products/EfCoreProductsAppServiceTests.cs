using ProductManagementSystem.Products;
using Xunit;

namespace ProductManagementSystem.EntityFrameworkCore.Applications.Products;

public class EfCoreProductsAppServiceTests : ProductsAppServiceTests<ProductManagementSystemEntityFrameworkCoreTestModule>
{
}