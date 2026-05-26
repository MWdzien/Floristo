using Floristo.Application.Categories.Exceptions;
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

internal sealed class BrowseProductsByCategoryHandler : IQueryHandler<BrowseProductsByCategory, PagedResult<StorefrontProductDto>>
{
    private readonly DbSet<ProductReadModel> _products;
    private readonly DbSet<CategoryReadModel> _categories;

    public BrowseProductsByCategoryHandler(ReadDbContext dbContext)
    {
        _products = dbContext.Products;
        _categories = dbContext.Categories;
    }
    
    public async Task<PagedResult<StorefrontProductDto>> HandleAsync(BrowseProductsByCategory query)
    {
        var category = await _categories
            .Where(c => c.Slug == query.CategorySlug && c.UnpublishedAt == null)
            .Select(c => new {c.Id})
            .SingleOrDefaultAsync();

        if (category == null)
            throw new CategoryNotFoundException(query.CategorySlug);
        
        var products = _products
            .AsNoTracking()
            .Where(p => p.CategoryId == category.Id && p.UnpublishedAt == null);

        var sorted = products.ApplySort(query.Sort);

        var result = sorted
            .Select(p => p.AsDto());

        return await result.ToPagedResultAsync(query);
    }
}