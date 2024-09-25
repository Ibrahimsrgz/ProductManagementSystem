using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ProductManagementSystem.Products;

namespace ProductManagementSystem.Products
{
    public class ProductsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ProductsDataSeedContributor(IProductRepository productRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _productRepository = productRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _productRepository.InsertAsync(new Product
            (
                name: "e4251926d5734d70adf54485a5f3993e7c945b4c8c334e0b8f437a777358c5924e876092acaa497b890884f76c748a56f6dc",
                code: "01ddd2469c",
                price: 789244552,
                quantity: 1665247642
            ));

            await _productRepository.InsertAsync(new Product
            (
                name: "41d3f19511044233b2ba9658283524ebe3141284b512499c9e3419accc90c64801f710ac256248b8b88d5582a69d401ee628",
                code: "7397902b8c",
                price: 2000021051,
                quantity: 237450434
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}