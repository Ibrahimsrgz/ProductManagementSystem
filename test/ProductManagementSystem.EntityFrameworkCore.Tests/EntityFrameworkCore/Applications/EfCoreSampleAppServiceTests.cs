using ProductManagementSystem.Samples;
using Xunit;

namespace ProductManagementSystem.EntityFrameworkCore.Applications;

[Collection(ProductManagementSystemTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<ProductManagementSystemEntityFrameworkCoreTestModule>
{

}
