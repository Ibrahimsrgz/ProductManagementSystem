using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ProductManagementSystem.Currencies;
using ProductManagementSystem.EntityFrameworkCore;
using Xunit;

namespace ProductManagementSystem.EntityFrameworkCore.Domains.Currencies
{
    public class CurrencyRepositoryTests : ProductManagementSystemEntityFrameworkCoreTestBase
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyRepositoryTests()
        {
            _currencyRepository = GetRequiredService<ICurrencyRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _currencyRepository.GetListAsync(
                    code: "595",
                    name: "2fc36d1507ae4cadba37829ef15592e72fa8a463be8a44b79a",
                    symbol: "94ee297a1e",
                    country: "c71e8d31c86c480ebcff5b0b2db6ca58c17b09ac969e489e8c",
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("9a2f6900-5be0-4499-8583-96f994c2ddd6"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _currencyRepository.GetCountAsync(
                    code: "f39",
                    name: "265b057ddb55406ca23ed860ce878d6710b2af845dfc4a9fae",
                    symbol: "70c8fb5b07",
                    country: "e625f2a744544d36b13f0a8b45460cbcc891a3c14e104187ac",
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}