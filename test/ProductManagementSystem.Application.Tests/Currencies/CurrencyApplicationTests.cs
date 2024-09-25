using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace ProductManagementSystem.Currencies
{
    public abstract class CurrenciesAppServiceTests<TStartupModule> : ProductManagementSystemApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly ICurrenciesAppService _currenciesAppService;
        private readonly IRepository<Currency, Guid> _currencyRepository;

        public CurrenciesAppServiceTests()
        {
            _currenciesAppService = GetRequiredService<ICurrenciesAppService>();
            _currencyRepository = GetRequiredService<IRepository<Currency, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _currenciesAppService.GetListAsync(new GetCurrenciesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("9a2f6900-5be0-4499-8583-96f994c2ddd6")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("b5c04c89-98b2-409d-af41-5254489d5e3d")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _currenciesAppService.GetAsync(Guid.Parse("9a2f6900-5be0-4499-8583-96f994c2ddd6"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("9a2f6900-5be0-4499-8583-96f994c2ddd6"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CurrencyCreateDto
            {
                Code = "b96",
                Numeric = 1186272952,
                Name = "4c7f8ad979c44aac8331e1ba5200db565ba919aed0254f1c8e",
                Symbol = "ed433fe5bb",
                Country = "ae957aefad654b81a8b73e7922ec6ba36ed4a3d28b8340b8a9",
                Active = true,
                Order = 2068653325
            };

            // Act
            var serviceResult = await _currenciesAppService.CreateAsync(input);

            // Assert
            var result = await _currencyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("b96");
            result.Numeric.ShouldBe(1186272952);
            result.Name.ShouldBe("4c7f8ad979c44aac8331e1ba5200db565ba919aed0254f1c8e");
            result.Symbol.ShouldBe("ed433fe5bb");
            result.Country.ShouldBe("ae957aefad654b81a8b73e7922ec6ba36ed4a3d28b8340b8a9");
            result.Active.ShouldBe(true);
            result.Order.ShouldBe(2068653325);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CurrencyUpdateDto()
            {
                Code = "183",
                Numeric = 1349547879,
                Name = "f5c33df539aa46eea31af94d7f19c915353d510e93b54e75a7",
                Symbol = "bb3b687fdc",
                Country = "294328a8ac5c4b8496e95771b607f17effc3c6d43f554582b7",
                Active = true,
                Order = 484877616
            };

            // Act
            var serviceResult = await _currenciesAppService.UpdateAsync(Guid.Parse("9a2f6900-5be0-4499-8583-96f994c2ddd6"), input);

            // Assert
            var result = await _currencyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("183");
            result.Numeric.ShouldBe(1349547879);
            result.Name.ShouldBe("f5c33df539aa46eea31af94d7f19c915353d510e93b54e75a7");
            result.Symbol.ShouldBe("bb3b687fdc");
            result.Country.ShouldBe("294328a8ac5c4b8496e95771b607f17effc3c6d43f554582b7");
            result.Active.ShouldBe(true);
            result.Order.ShouldBe(484877616);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _currenciesAppService.DeleteAsync(Guid.Parse("9a2f6900-5be0-4499-8583-96f994c2ddd6"));

            // Assert
            var result = await _currencyRepository.FindAsync(c => c.Id == Guid.Parse("9a2f6900-5be0-4499-8583-96f994c2ddd6"));

            result.ShouldBeNull();
        }
    }
}