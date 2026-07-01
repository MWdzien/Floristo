using Floristo.Application.Categories.DTOs;
using Floristo.Application.Categories.Queries;
using Floristo.Infrastructure.EF.Categories.Models;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.SharedAbstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Categories.Queries.Handlers;

internal sealed class GetCategoryNavigationHandler : IQueryHandler<GetCategoryNavigation, IEnumerable<CategoryNavigationItemDto>>
{
    private readonly DbSet<CategoryReadModel> _categories;

    public GetCategoryNavigationHandler(ReadDbContext dbContext)
    {
        _categories = dbContext.Categories;
    }

    public async Task<IEnumerable<CategoryNavigationItemDto>> HandleAsync(GetCategoryNavigation query)
    {
        var rows = await _categories
            .Where(c => c.PublishedAt != null)
            .OrderBy(c => c.SortOrder)
            .AsNoTracking()
            .Select(c => new { c.Id, c.ParentId, c.Name, c.Slug, c.ImageUrl })
            .ToListAsync();

        var byParent = rows
            .Where(r => r.ParentId != null)
            .GroupBy(r => r.ParentId!.Value)
            .ToDictionary(g => g.Key, g => g.ToList());

        return rows
            .Where(r => r.ParentId == null)
            .Select(root => new CategoryNavigationItemDto(
                root.Name,
                root.Slug,
                root.ImageUrl,
                byParent.TryGetValue(root.Id, out var kids)
                    ? kids.Select(k => new CategoryNavigationItemDto(k.Name, k.Slug, k.ImageUrl, [])).ToList()
                    : []))
            .ToList();
    }
}