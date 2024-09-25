using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ProductManagementSystem.Products
{
    public abstract class ProductManagerBase : DomainService
    {
        protected IProductRepository _productRepository;

        public ProductManagerBase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public virtual async Task<Product> CreateAsync(
        string name, string code, decimal price, int quantity)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), ProductConsts.NameMaxLength);
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), ProductConsts.CodeMaxLength);

            var product = new Product(

             name, code, price, quantity
             );

            return await _productRepository.InsertAsync(product);
        }

        public virtual async Task<Product> UpdateAsync(
            long id,
            string name, string code, decimal price, int quantity, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), ProductConsts.NameMaxLength);
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), ProductConsts.CodeMaxLength);

            var product = await _productRepository.GetAsync(id);

            product.Name = name;
            product.Code = code;
            product.Price = price;
            product.Quantity = quantity;

            product.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _productRepository.UpdateAsync(product);
        }

    }
}