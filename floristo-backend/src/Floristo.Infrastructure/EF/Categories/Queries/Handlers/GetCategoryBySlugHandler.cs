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
        => await (
                from c in _categories.AsNoTracking()
                where c.Slug == query.Slug && c.PublishedAt != null
                join p in _categories.Where(x => x.PublishedAt != null) on c.ParentId equals p.Id into parents
                from p in parents.DefaultIfEmpty()
                select new StorefrontCategoryDetailsDto(
                    c.Name,
                    c.Slug,
                    p != null ? p.Slug : null,
                    c.Description,
                    c.ImageUrl,
                    c.SeoTitle,
                    c.SeoDescription))
            .SingleOrDefaultAsync();
}