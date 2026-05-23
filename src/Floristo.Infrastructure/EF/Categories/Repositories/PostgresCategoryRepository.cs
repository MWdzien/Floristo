using Floristo.Domain.Categories.Entities;
using Floristo.Domain.Categories.Repositories;
using Floristo.Domain.Categories.ValueObjects;
using Floristo.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Categories.Repositories;

internal sealed class PostgresCategoryRepository : ICategoryRepository
{
    private readonly DbSet<Category> _categories;
    private readonly WriteDbContext _dbContext;

    public PostgresCategoryRepository(WriteDbContext dbContext)
    {
        _categories = dbContext.Categories;
        _dbContext = dbContext;
    }

    public Task<Category?> GetAsync(CategoryId id)
        => _categories.SingleOrDefaultAsync(c => c.Id == id);

    public async Task AddAsync(Category category)
    {
        await _categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _categories.Update(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category category)
    {
        _categories.Remove(category);
        await _dbContext.SaveChangesAsync();
    }
}