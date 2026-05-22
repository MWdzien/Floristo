using Floristo.Application.Categories.DTOs;
using Floristo.Application.Categories.Queries;
using Floristo.Infrastructure.EF.Categories.Models;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.SharedAbstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Categories.Queries.Handlers;

internal sealed class GetCategoryBySlugHandler : IQueryHandler<GetCategoryBySlug, StorefrontCategoryDetailsDto?>
{
    private readonly DbSet<CategoryReadModel> _categories;

    public GetCategoryBySlugHandler(ReadDbContext dbContext)
    {
        _categories = dbContext.Categories;
    }

    public async Task<StorefrontCategoryDetailsDto?> HandleAsync(GetCategoryBySlug query)
        => await _categories
            .Where(c => c.Slug == query.Slug && c.UnpublishedAt == null)
            .AsNoTracking()
            .Select(c =>
                new StorefrontCategoryDetailsDto(c.Name, c.Slug, c.Description, c.ImageUrl, c.SeoTitle, c.SeoDescription)
            )
            .SingleOrDefaultAsync();
}