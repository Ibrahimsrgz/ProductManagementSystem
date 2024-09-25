using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace ProductManagementSystem.Products
{
    public abstract class ProductsAppServiceTests<TStartupModule> : ProductManagementSystemApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IProductsAppService _productsAppService;
        private readonly IRepository<Product, long> _productRepository;

        public ProductsAppServiceTests()
        {
            _productsAppService = GetRequiredService<IProductsAppService>();
            _productRepository = GetRequiredService<IRepository<Product, long>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _productsAppService.GetListAsync(new GetProductsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Product.Id == 1).ShouldBe(true);
            result.Items.Any(x => x.Product.Id == 2).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _productsAppService.GetAsync(1);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(1);
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ProductCreateDto
            {
                Name = "0cc6495ad5fa41a8b26de12ee3c4bbef2c6ba40ddfb347ed826264ba4de25eca4a8ef2bdfda649e8a9d0c7cb3a11940c6fbd",
                Code = "896f0ea13f",
                Price = 560648261,
                Quantity = 479221319,
                CurrencyId = Guid.Parse("9a2f6900-5be0-4499-8583-96f994c2ddd6")
            };

            // Act
            var serviceResult = await _productsAppService.CreateAsync(input);

            // Assert
            var result = await _productRepository.FindAsync(c => c.Name == serviceResult.Name);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("0cc6495ad5fa41a8b26de12ee3c4bbef2c6ba40ddfb347ed826264ba4de25eca4a8ef2bdfda649e8a9d0c7cb3a11940c6fbd");
            result.Code.ShouldBe("896f0ea13f");
            result.Price.ShouldBe(560648261);
            result.Quantity.ShouldBe(479221319);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ProductUpdateDto()
            {
                Name = "28f98bd621854eb789f8006f79c9d6495a0bcb612d1743608ee0062d221b6907242bcee88de44139a4056d9d3c7570be1edf",
                Code = "f8c524a5d3",
                Price = 59718018,
                Quantity = 1743921018,
                CurrencyId = Guid.Parse("9a2f6900-5be0-4499-8583-96f994c2ddd6")
            };

            // Act
            var serviceResult = await _productsAppService.UpdateAsync(1, input);

            // Assert
            var result = await _productRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("28f98bd621854eb789f8006f79c9d6495a0bcb612d1743608ee0062d221b6907242bcee88de44139a4056d9d3c7570be1edf");
            result.Code.ShouldBe("f8c524a5d3");
            result.Price.ShouldBe(59718018);
            result.Quantity.ShouldBe(1743921018);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _productsAppService.DeleteAsync(1);

            // Assert
            var result = await _productRepository.FindAsync(c => c.Id == 1);

            result.ShouldBeNull();
        }
    }
}