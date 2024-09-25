using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using ProductManagementSystem.EntityFrameworkCore;

namespace ProductManagementSystem.Currencies
{
    public class EfCoreCurrencyRepository : EfCoreCurrencyRepositoryBase, ICurrencyRepository
    {
        public EfCoreCurrencyRepository(IDbContextProvider<ProductManagementSystemDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}