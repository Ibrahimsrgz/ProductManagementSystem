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
                    name: "941b003a948a426cbda5386d5d12b5bc1c6783e71c8e45c0b9e72b7f96fba97616e39b8eec4443d8a42d1394c6a681a554fe",
                    code: "63cf2c0228"
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
                    name: "0f2a1f00e4a34148845d9ca00e4638165132ab71c59944e4bd3282a355c6996db8e45cd189654e6198cc33e96963c59a3cd7",
                    code: "e2d7083d16"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}