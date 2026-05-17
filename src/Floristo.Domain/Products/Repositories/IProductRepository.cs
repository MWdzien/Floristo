using Floristo.Domain.Products.Entities;
using Floristo.Domain.Products.ValueObjects;

namespace Floristo.Domain.Products.Repositories;

public interface IProductRepository
{
    Task<Product?> GetAsync(ProductId id);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Product product);
}