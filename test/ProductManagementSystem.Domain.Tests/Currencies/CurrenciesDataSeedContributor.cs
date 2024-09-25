using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ProductManagementSystem.Currencies;

namespace ProductManagementSystem.Currencies
{
    public class CurrenciesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CurrenciesDataSeedContributor(ICurrencyRepository currencyRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _currencyRepository = currencyRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _currencyRepository.InsertAsync(new Currency
            (
                id: Guid.Parse("9a2f6900-5be0-4499-8583-96f994c2ddd6"),
                code: "595",
                numeric: 223125332,
                name: "2fc36d1507ae4cadba37829ef15592e72fa8a463be8a44b79a",
                symbol: "94ee297a1e",
                country: "c71e8d31c86c480ebcff5b0b2db6ca58c17b09ac969e489e8c",
                active: true,
                order: 1871850919
            ));

            await _currencyRepository.InsertAsync(new Currency
            (
                id: Guid.Parse("b5c04c89-98b2-409d-af41-5254489d5e3d"),
                code: "f39",
                numeric: 100297004,
                name: "265b057ddb55406ca23ed860ce878d6710b2af845dfc4a9fae",
                symbol: "70c8fb5b07",
                country: "e625f2a744544d36b13f0a8b45460cbcc891a3c14e104187ac",
                active: true,
                order: 659310321
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}