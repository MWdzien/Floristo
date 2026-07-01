using Floristo.Application.Categories.DTOs;
using Floristo.Application.Categories.Queries;
using Floristo.Infrastructure.EF.Categories.Models;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.SharedAbstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Categories.Queries.Handlers;

internal sealed class GetCategoryForBackofficeHandler : IQueryHandler<GetCategoryForBackoffice, BackofficeCategoryDetailsDto?>
{
    private readonly DbSet<CategoryReadModel> _categories;

    public GetCategoryForBackofficeHandler(ReadDbContext dbContext)
    {
        _categories = dbContext.Categories;
    }

    public async Task<BackofficeCategoryDetailsDto?> HandleAsync(GetCategoryForBackoffice query)
        => await _categories
            .Where(c => c.Id == query.Id)
            .AsNoTracking()
            .Select(c =>
                new BackofficeCategoryDetailsDto(c.Id, c.Name, c.Description, c.Slug, c.SortOrder, c.ImageUrl,
                    c.SeoTitle, c.SeoDescription, c.CreatedAt, c.PublishedAt)
            )
            .SingleOrDefaultAsync();
}