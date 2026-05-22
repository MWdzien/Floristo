using Floristo.Application.Categories.DTOs;
using Floristo.Application.Categories.Queries;
using Floristo.Infrastructure.EF.Categories.Models;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.SharedAbstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Categories.Queries.Handlers;

internal sealed class GetCategoriesForBackofficeHandler : IQueryHandler<GetCategoriesForBackoffice, IEnumerable<BackofficeCategoryDto>>
{
    private readonly DbSet<CategoryReadModel> _categories;

    public GetCategoriesForBackofficeHandler(ReadDbContext dbContext)
    {
        _categories = dbContext.Categories;
    }

    public async Task<IEnumerable<BackofficeCategoryDto>> HandleAsync(GetCategoriesForBackoffice query)
    {
        var categories = _categories.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.SearchPhrase))
        {
            categories = categories.Where(c =>
                Microsoft.EntityFrameworkCore.EF.Functions.ILike(c.Name, $"%{query.SearchPhrase}%"));
        }

        if (query.IsPublished.HasValue)
        {
            categories = query.IsPublished.Value
                ? categories.Where(c => c.UnpublishedAt != null)
                : categories.Where(c => c.UnpublishedAt == null);
        }
        
        return await categories
            .AsNoTracking()
            .OrderBy(c => c.SortOrder)
            .Select(c => 
                new BackofficeCategoryDto(c.Id, c.Name, c.ImageUrl, c.UnpublishedAt == null, c.SortOrder)
            )
            .ToListAsync();
    }
}