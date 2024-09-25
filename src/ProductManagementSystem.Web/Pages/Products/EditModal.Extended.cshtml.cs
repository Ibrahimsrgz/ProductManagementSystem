using ProductManagementSystem.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ProductManagementSystem.Products;

namespace ProductManagementSystem.Web.Pages.Products
{
    public class EditModalModel : EditModalModelBase
    {
        public EditModalModel(IProductsAppService productsAppService)
            : base(productsAppService)
        {
        }
    }
}