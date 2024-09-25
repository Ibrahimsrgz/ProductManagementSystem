using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ProductManagementSystem.Currencies
{
    public abstract class CurrencyBase : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Code { get; set; }

        public virtual int Numeric { get; set; }

        [CanBeNull]
        public virtual string? Name { get; set; }

        [CanBeNull]
        public virtual string? Symbol { get; set; }

        [CanBeNull]
        public virtual string? Country { get; set; }

        public virtual bool Active { get; set; }

        public virtual int Order { get; set; }

        protected CurrencyBase()
        {

        }

        public CurrencyBase(Guid id, string code, int numeric, bool active, int order, string? name = null, string? symbol = null, string? country = null)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), CurrencyConsts.CodeMaxLength, 0);
            Check.Length(name, nameof(name), CurrencyConsts.NameMaxLength, 0);
            Check.Length(symbol, nameof(symbol), CurrencyConsts.SymbolMaxLength, 0);
            Check.Length(country, nameof(country), CurrencyConsts.CountryMaxLength, 0);
            Code = code;
            Numeric = numeric;
            Active = active;
            Order = order;
            Name = name;
            Symbol = symbol;
            Country = country;
        }

    }
}