using ProductManagementSystem.Samples;
using Xunit;

namespace ProductManagementSystem.EntityFrameworkCore.Domains;

[Collection(ProductManagementSystemTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<ProductManagementSystemEntityFrameworkCoreTestModule>
{

}
