using Floristo.Domain.Products.Entities;
using Floristo.Domain.Products.Repositories;
using Floristo.Domain.Products.ValueObjects;
using Floristo.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Products.Repositories;

internal sealed class PostgresProductRepository : IProductRepository
{
    private readonly DbSet<Product> _products;
    private readonly WriteDbContext _dbContext;

    public PostgresProductRepository(WriteDbContext dbContext)
    {
        _products = dbContext.Products;
        _dbContext = dbContext;
    }

    public Task<Product?> GetAsync(ProductId id)
        => _products.Include(p => p.Images).SingleOrDefaultAsync(p => p.Id == id);

    public async Task AddAsync(Product product)
    {
        await _products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _products.Update(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _products.Remove(product);
        await _dbContext.SaveChangesAsync();
    }
}