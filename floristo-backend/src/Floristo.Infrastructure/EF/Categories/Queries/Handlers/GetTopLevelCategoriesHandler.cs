using Floristo.Application.Categories.DTOs;
using Floristo.Application.Categories.Queries;
using Floristo.Infrastructure.EF.Categories.Models;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.SharedAbstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Categories.Queries.Handlers;

internal sealed class GetTopLevelCategoriesHandler : IQueryHandler<GetTopLevelCategories, IEnumerable<TopLevelCategoryDto>>
{
    private readonly DbSet<CategoryReadModel> _categories;

    public GetTopLevelCategoriesHandler(ReadDbContext dbContext)
    {
        _categories = dbContext.Categories;
    }

    public async Task<IEnumerable<TopLevelCategoryDto>> HandleAsync(GetTopLevelCategories query)
        => await _categories
            .Where(c => c.PublishedAt != null && c.ParentId == null)
            .OrderBy(c => c.SortOrder)
            .AsNoTracking()
            .Select(c => new TopLevelCategoryDto(c.Name, c.Slug, c.ImageUrl, c.ShortDescription))
            .ToListAsync();
}