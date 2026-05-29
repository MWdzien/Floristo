using Floristo.Application.Products.DTOs;
using Floristo.Application.Products.Exceptions;
using Floristo.Application.Products.Queries;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.Infrastructure.EF.Products.Models;
using Floristo.SharedAbstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Products.Queries.Handlers;

internal sealed class GetRelatedProductsHandler : IQueryHandler<GetRelatedProducts, IReadOnlyList<StorefrontProductDto>>
{
    private readonly DbSet<ProductReadModel> _products;

    public GetRelatedProductsHandler(ReadDbContext dbContext)
    {
        _products = dbContext.Products;
    }

    public async Task<IReadOnlyList<StorefrontProductDto>> HandleAsync(GetRelatedProducts query)
    {
        var product = await _products
            .AsNoTracking()
            .Where(p => p.Slug == query.Slug && p.UnpublishedAt == null)
            .Select(p => new { p.CategoryId })
            .SingleOrDefaultAsync();

        if (product is null)
            throw new ProductNotFoundException(query.Slug);

        // take related products from the same category
        var sameCategory = await _products
            .AsNoTracking()
            .Where(p => p.CategoryId == product.CategoryId
                        && p.Slug != query.Slug
                        && p.UnpublishedAt == null
            )
            .OrderBy(_ => Guid.NewGuid())
            .Take(query.Limit)
            .AsDto()
            .ToListAsync();

        if (sameCategory.Count >= query.Limit)
            return sameCategory;
        
        // if there aren't enough products in the same category, fallback to other categories
        var remaining = query.Limit - sameCategory.Count;

        var fallback = await _products
            .AsNoTracking()
            .Where(p => p.Slug != query.Slug
                        && p.CategoryId != product.CategoryId
                        && p.UnpublishedAt == null
            )
            .OrderBy(_ => Guid.NewGuid())
            .Take(remaining)
            .AsDto()
            .ToListAsync();

        return sameCategory.Concat(fallback).ToList();
    }
}