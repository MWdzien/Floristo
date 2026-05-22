using Floristo.Application.Categories.DTOs;
using Floristo.Application.Categories.Queries;
using Floristo.Infrastructure.EF.Categories.Models;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.SharedAbstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Categories.Queries.Handlers;

internal sealed class BrowseCategoriesHandler : IQueryHandler<BrowseCategories, IEnumerable<StorefrontCategoryDto>>
{
    private readonly DbSet<CategoryReadModel> _categories;

    public BrowseCategoriesHandler(ReadDbContext dbContext)
    {
        _categories = dbContext.Categories;
    }

    public async Task<IEnumerable<StorefrontCategoryDto>> HandleAsync(BrowseCategories query)
        => await _categories
            .Where(c => c.UnpublishedAt == null)
            .OrderBy(c => c.SortOrder)
            .AsNoTracking()
            .Select(c => new StorefrontCategoryDto(c.Name, c.Slug, c.ImageUrl))
            .ToListAsync();
}