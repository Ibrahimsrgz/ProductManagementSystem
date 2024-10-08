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
    public class Currency : CurrencyBase
    {
        //<suite-custom-code-autogenerated>
        protected Currency()
        {

        }

        public Currency(Guid id, string code, int numeric, bool active, int order, string? name = null, string? symbol = null, string? country = null)
            : base(id, code, numeric, active, order, name, symbol, country)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}