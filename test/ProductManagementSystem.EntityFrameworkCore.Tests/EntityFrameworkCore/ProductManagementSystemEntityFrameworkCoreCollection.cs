﻿using Xunit;

namespace ProductManagementSystem.EntityFrameworkCore;

[CollectionDefinition(ProductManagementSystemTestConsts.CollectionDefinitionName)]
public class ProductManagementSystemEntityFrameworkCoreCollection : ICollectionFixture<ProductManagementSystemEntityFrameworkCoreFixture>
{

}
