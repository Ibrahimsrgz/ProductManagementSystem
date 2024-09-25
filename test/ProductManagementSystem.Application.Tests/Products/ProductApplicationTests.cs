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
            result.Items.Any(x => x.Id == 1).ShouldBe(true);
            result.Items.Any(x => x.Id == 2).ShouldBe(true);
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
                Name = "a4c9f0a11beb415484b762b517ec40708556425628ea4235842ca4128ce33df2d5b27cb647844aabad54a58a317af0cde28e",
                Code = "ac00eabdec",
                Price = 1198045759,
                Quantity = 1219000513
            };

            // Act
            var serviceResult = await _productsAppService.CreateAsync(input);

            // Assert
            var result = await _productRepository.FindAsync(c => c.Name == serviceResult.Name);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("a4c9f0a11beb415484b762b517ec40708556425628ea4235842ca4128ce33df2d5b27cb647844aabad54a58a317af0cde28e");
            result.Code.ShouldBe("ac00eabdec");
            result.Price.ShouldBe(1198045759);
            result.Quantity.ShouldBe(1219000513);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ProductUpdateDto()
            {
                Name = "0a173dc915a844f2b441f8add019f023547df9ea9741468f99fb9a0bb9e2189475fb92ef4e794995b43c0dc18270836152b0",
                Code = "1138d2d8d2",
                Price = 404773372,
                Quantity = 1336379877
            };

            // Act
            var serviceResult = await _productsAppService.UpdateAsync(1, input);

            // Assert
            var result = await _productRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("0a173dc915a844f2b441f8add019f023547df9ea9741468f99fb9a0bb9e2189475fb92ef4e794995b43c0dc18270836152b0");
            result.Code.ShouldBe("1138d2d8d2");
            result.Price.ShouldBe(404773372);
            result.Quantity.ShouldBe(1336379877);
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