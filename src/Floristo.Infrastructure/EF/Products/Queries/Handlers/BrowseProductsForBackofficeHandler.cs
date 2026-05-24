using Floristo.Application.Products.DTOs;
using Floristo.Application.Products.Queries;
using Floristo.Infrastructure.EF.Categories.Models;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.Infrastructure.EF.Products.Models;
using Floristo.Shared.Paging;
using Floristo.SharedAbstractions.Paging;
using Floristo.SharedAbstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Products.Queries.Handlers;

internal sealed class BrowseProductsForBackofficeHandler : IQueryHandler<BrowseProductsForBackoffice, PagedResult<BackofficeProductDto>>
{
    private readonly DbSet<ProductReadModel> _products;
    private readonly DbSet<CategoryReadModel> _categories;

    public BrowseProductsForBackofficeHandler(ReadDbContext dbContext)
    {
        _products = dbContext.Products;
        _categories = dbContext.Categories;
    }

    public async Task<PagedResult<BackofficeProductDto>> HandleAsync(BrowseProductsForBackoffice query)
    {
        var products = _products.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.SearchPhrase))
            products = products.Where(p =>
                Microsoft.EntityFrameworkCore.EF.Functions.ILike(p.Name, $"%{query.SearchPhrase}%"));

        if (query.CategoryId.HasValue)
            products = products.Where(p => p.CategoryId == query.CategoryId.Value);

        if (query.IsPublished.HasValue)
            products = query.IsPublished.Value
                ? products.Where(p => p.UnpublishedAt == null)
                : products.Where(p => p.UnpublishedAt != null);

        var sorted = products.ApplySort(query.Sort);
        
        var result = from p in sorted
            join c in _categories on p.CategoryId equals c.Id
            select new BackofficeProductDto(
                p.Id,
                p.Name,
                p.Slug,
                p.Price,
                c.Name,
                p.UnpublishedAt == null,
                p.Images
                    .OrderBy(i => i.SortOrder)
                    .Select(i => i.Url)
                    .FirstOrDefault()
            );

        return await result.ToPagedResultAsync(query);
    }
}