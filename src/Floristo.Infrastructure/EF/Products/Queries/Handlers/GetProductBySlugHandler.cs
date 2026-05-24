using Floristo.Application.Products.DTOs;
using Floristo.Application.Products.Exceptions;
using Floristo.Application.Products.Queries;
using Floristo.Infrastructure.EF.Categories.Models;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.Infrastructure.EF.Products.Models;
using Floristo.SharedAbstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Products.Queries.Handlers;

internal sealed class GetProductBySlugHandler : IQueryHandler<GetProductBySlug, StorefrontProductDetailsDto>
{
    private readonly DbSet<ProductReadModel> _products;
    private readonly DbSet<CategoryReadModel> _categories;

    public GetProductBySlugHandler(ReadDbContext dbContext)
    {
        _products = dbContext.Products;
        _categories = dbContext.Categories;
    }

    public async Task<StorefrontProductDetailsDto> HandleAsync(GetProductBySlug query)
    {
        var result = await (from p in _products.AsNoTracking()
                where p.Slug == query.Slug && p.UnpublishedAt == null
                join c in _categories on p.CategoryId equals c.Id
                select new
                {
                    p.Id,
                    p.Name,
                    p.Slug,
                    p.Description,
                    p.Price,
                    CategorySlug = c.Slug,
                    CategoryName = c.Name,
                    p.Height,
                    p.Width,
                    p.Depth,
                    p.Diameter,
                    p.Length,
                    p.SeoTitle,
                    p.SeoDescription,
                    p.AllowEngraving,
                    p.AllowGiftTag,
                    Images = p.Images
                        .OrderBy(i => i.SortOrder)
                        .Select(i => new ProductImageDto(i.Id, i.Url, i.SortOrder))
                        .ToList()
                })
            .SingleOrDefaultAsync();

        if (result is null)
            throw new ProductNotFoundException(query.Slug);

        var dimensions = result.Height.HasValue || result.Width.HasValue || result.Depth.HasValue
                         || result.Diameter.HasValue || result.Length.HasValue
            ? new ProductDimensionsDto(result.Height, result.Width, result.Depth, result.Diameter, result.Length)
            : null;

        return new StorefrontProductDetailsDto(
            result.Id,
            result.Name,
            result.Slug,
            result.Description,
            result.Price,
            result.CategorySlug,
            result.CategoryName,
            dimensions,
            result.SeoTitle,
            result.SeoDescription,
            result.AllowEngraving,
            result.AllowGiftTag,
            result.Images
            );
    }
}