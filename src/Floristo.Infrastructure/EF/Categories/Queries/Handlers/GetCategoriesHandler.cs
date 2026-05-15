using Floristo.Application.Categories.DTOs;
using Floristo.Application.Categories.Queries;
using Floristo.Infrastructure.EF.Categories.Models;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.SharedAbstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Categories.Queries.Handlers;

internal sealed class GetCategoriesHandler : IQueryHandler<GetCategories, IEnumerable<CategoryDto>>
{
    private readonly DbSet<CategoryReadModel> _categories;

    public GetCategoriesHandler(ReadDbContext dbContext)
    {
        _categories = dbContext.Categories;
    }

    public async Task<IEnumerable<CategoryDto>> HandleAsync(GetCategories query)
    {
        var categories = _categories.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.SearchPhrase))
        {
            var searchPhrase = query.SearchPhrase;
            categories = categories.Where(c =>
                Microsoft.EntityFrameworkCore.EF.Functions.ILike(c.Name, $"%{searchPhrase}%")
            );
        }
        
        if (query.IsActive.HasValue)
        {
            categories = query.IsActive.Value
                ? categories.Where(c => c.DeactivatedAt == null)
                : categories.Where(c => c.DeactivatedAt != null);
        }
        
        return await categories.AsNoTracking().Select(c => c.AsDto()).ToListAsync();
    }
}