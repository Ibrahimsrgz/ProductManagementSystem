using System;

namespace ProductManagementSystem.Currencies;

public abstract class CurrencyDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}