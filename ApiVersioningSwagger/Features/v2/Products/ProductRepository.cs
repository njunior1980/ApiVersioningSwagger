using Bogus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiVersioningSwagger.Features.v2.Products
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductReadModel>> GetProducts();
        Task<IEnumerable<ProductReadModelMobile>> GetProductsMobile();
    }

    public class ProductRepository : IProductRepository
    {
        public async Task<IEnumerable<ProductReadModel>> GetProducts()
        {
            var result = new Faker<ProductReadModel>()
               .RuleFor(p => p.Id, Guid.NewGuid().ToString())
               .RuleFor(p => p.Name, p => p.Commerce.ProductName())
               .RuleFor(p => p.Departament, p => p.Commerce.Department())
               .RuleFor(p => p.Price, p => p.Commerce.Price(100, 1000, 2, " $"))
               .RuleFor(p => p.Sku, p => p.Commerce.Ean8())
               .GenerateLazy(10);

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<ProductReadModelMobile>> GetProductsMobile()
        {
            var result = new Faker<ProductReadModelMobile>()
               .RuleFor(p => p.Id, Guid.NewGuid().ToString())
               .RuleFor(p => p.Name, p => p.Commerce.Product())
               .RuleFor(p => p.Sku, p => p.Commerce.Ean8())
               .GenerateLazy(10);

            return await Task.FromResult(result);
        }
    }
}
