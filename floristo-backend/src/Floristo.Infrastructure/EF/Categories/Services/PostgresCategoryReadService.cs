using Floristo.Application.Categories.Services;
using Floristo.Infrastructure.EF.Categories.Models;
using Floristo.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Categories.Services;

internal sealed class PostgresCategoryReadService : ICategoryReadService
{
    private readonly DbSet<CategoryReadModel> _categories;

    public PostgresCategoryReadService(ReadDbContext dbContext)
    {
        _categories = dbContext.Categories;
    }

    public Task<bool> ExistsBySlugAsync(string slug, Guid? excludeId = null)
        => _categories.AnyAsync(c => c.Slug == slug && (excludeId == null || c.Id != excludeId));

    public Task<bool> ExistsAsync(Guid id)
        => _categories.AnyAsync(c => c.Id == id);

    public Task<bool> IsRootAsync(Guid id)
        => _categories.AnyAsync(c => c.Id == id && c.ParentId == null);

    public Task<bool> HasChildrenAsync(Guid parentId)
        => _categories.AnyAsync(c => c.ParentId == parentId);

    public async Task<int> GetNextSortOrderAsync(Guid? parentId)
    {
        var max = await _categories
            .Where(c => c.ParentId == parentId)
            .MaxAsync(c => (int?)c.SortOrder) ?? -1;

        return max + 1;
    }
}