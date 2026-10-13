using Floristo.Application.Products.Services;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.Infrastructure.EF.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Products.Services;

internal sealed class PostgresProductReadService : IProductReadService
{
    private readonly DbSet<ProductReadModel> _products;

    public PostgresProductReadService(ReadDbContext dbContext)
    {
        _products = dbContext.Products;
    }

    public Task<bool> ExistsBySlugAsync(string slug, Guid? excludeId = null)
        => _products.AnyAsync(p => p.Slug == slug && (excludeId == null || p.Id != excludeId));

    public Task<bool> ExistsByCategoryAsync(Guid categoryId)
        => _products.AnyAsync(p => p.CategoryId == categoryId);

    public Task<Guid?> GetIdBySlugAsync(string slug)
        => _products.Where(p => p.Slug == slug && p.PublishedAt != null).Select(p => (Guid?)p.Id)
            .SingleOrDefaultAsync();
}