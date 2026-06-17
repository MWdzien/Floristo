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
        => await _categories
            .Where(c => c.PublishedAt != null)
            .OrderBy(c => c.SortOrder)
            .AsNoTracking()
            .Select(c => new CategoryNavigationItemDto(c.Name, c.Slug, c.ImageUrl, new List<CategoryNavigationItemDto>()))
            .ToListAsync();
}