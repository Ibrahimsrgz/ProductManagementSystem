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
    public abstract class ProductBase : FullAuditedAggregateRoot<long>
    {
        [NotNull]
        public virtual string Name { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        public virtual decimal Price { get; set; }

        public virtual int Quantity { get; set; }
        public Guid CurrencyId { get; set; }

        protected ProductBase()
        {

        }

        public ProductBase(Guid currencyId, string name, string code, decimal price, int quantity)
        {

            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), ProductConsts.NameMaxLength, 0);
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), ProductConsts.CodeMaxLength, 0);
            Name = name;
            Code = code;
            Price = price;
            Quantity = quantity;
            CurrencyId = currencyId;
        }

    }
}