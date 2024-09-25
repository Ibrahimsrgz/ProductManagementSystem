using ProductManagementSystem.Currencies;
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
        private readonly CurrenciesDataSeedContributor _currenciesDataSeedContributor;

        public ProductsDataSeedContributor(IProductRepository productRepository, IUnitOfWorkManager unitOfWorkManager, CurrenciesDataSeedContributor currenciesDataSeedContributor)
        {
            _productRepository = productRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _currenciesDataSeedContributor = currenciesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _currenciesDataSeedContributor.SeedAsync(context);

            await _productRepository.InsertAsync(new Product
            (
                name: "941b003a948a426cbda5386d5d12b5bc1c6783e71c8e45c0b9e72b7f96fba97616e39b8eec4443d8a42d1394c6a681a554fe",
                code: "63cf2c0228",
                price: 883826256,
                quantity: 462196643,
                currencyId: Guid.Parse("9a2f6900-5be0-4499-8583-96f994c2ddd6")
            ));

            await _productRepository.InsertAsync(new Product
            (
                name: "0f2a1f00e4a34148845d9ca00e4638165132ab71c59944e4bd3282a355c6996db8e45cd189654e6198cc33e96963c59a3cd7",
                code: "e2d7083d16",
                price: 702347561,
                quantity: 867475868,
                currencyId: Guid.Parse("9a2f6900-5be0-4499-8583-96f994c2ddd6")
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}