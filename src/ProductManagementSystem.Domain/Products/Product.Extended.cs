using ProductManagementSystem.Currencies;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ProductManagementSystem.Products
{
    public class Product : ProductBase
    {
        //<suite-custom-code-autogenerated>
        protected Product()
        {

        }

        public Product(Guid currencyId, string name, string code, decimal price, int quantity)
            : base(currencyId, name, code, price, quantity)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}