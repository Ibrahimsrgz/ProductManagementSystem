using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ProductManagementSystem.Products;
using ProductManagementSystem.EntityFrameworkCore;
using Xunit;

namespace ProductManagementSystem.EntityFrameworkCore.Domains.Products
{
    public class ProductRepositoryTests : ProductManagementSystemEntityFrameworkCoreTestBase
    {
        private readonly IProductRepository _productRepository;

        public ProductRepositoryTests()
        {
            _productRepository = GetRequiredService<IProductRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _productRepository.GetListAsync(
                    name: "e4251926d5734d70adf54485a5f3993e7c945b4c8c334e0b8f437a777358c5924e876092acaa497b890884f76c748a56f6dc",
                    code: "01ddd2469c"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(1);
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _productRepository.GetCountAsync(
                    name: "41d3f19511044233b2ba9658283524ebe3141284b512499c9e3419accc90c64801f710ac256248b8b88d5582a69d401ee628",
                    code: "7397902b8c"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}