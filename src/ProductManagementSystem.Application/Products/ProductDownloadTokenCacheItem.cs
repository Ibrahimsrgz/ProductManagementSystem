using System;

namespace ProductManagementSystem.Products;

public abstract class ProductDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}