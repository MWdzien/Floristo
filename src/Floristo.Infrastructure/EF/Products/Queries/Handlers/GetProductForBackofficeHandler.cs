using Floristo.Application.Products.DTOs;
using Floristo.Application.Products.Exceptions;
using Floristo.Application.Products.Queries;
using Floristo.Infrastructure.EF.Categories.Models;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.Infrastructure.EF.Products.Models;
using Floristo.SharedAbstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Products.Queries.Handlers;

internal sealed class GetProductForBackofficeHandler : IQueryHandler<GetProductForBackoffice, BackofficeProductDetailsDto>
{
    private readonly DbSet<ProductReadModel> _products;
    private readonly DbSet<CategoryReadModel> _categories;

    public GetProductForBackofficeHandler(ReadDbContext dbContext)
    {
        _products = dbContext.Products;
        _categories = dbContext.Categories;
    }

    public async Task<BackofficeProductDetailsDto> HandleAsync(GetProductForBackoffice query)
    {
        var result = await (from p in _products.AsNoTracking()
                where p.Id == query.Id
                join c in _categories on p.CategoryId equals c.Id
                select new
                {
                    p.Id,
                    p.Name,
                    p.Slug,
                    p.Description,
                    p.Price,
                    CategoryId = c.Id,
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
                    p.SortOrder,
                    p.CreatedAt,
                    p.UnpublishedAt,
                    Images = p.Images
                        .OrderBy(i => i.SortOrder)
                        .Select(i => new ProductImageDto(i.Id, i.Url, i.SortOrder))
                        .ToList(),
                    p.Version
                })
            .SingleOrDefaultAsync();

        if (result is null)
            throw new ProductNotFoundException(query.Id);

        var dimensions = result.Height.HasValue || result.Width.HasValue || result.Depth.HasValue
                         || result.Diameter.HasValue || result.Length.HasValue
            ? new ProductDimensionsDto(result.Height, result.Width, result.Depth, result.Diameter, result.Length)
            : null;

        return new BackofficeProductDetailsDto(
            result.Id,
            result.Name,
            result.Slug,
            result.Description,
            result.Price,
            result.CategoryId,
            result.CategoryName,
            dimensions,
            result.SeoTitle,
            result.SeoDescription,
            result.AllowEngraving,
            result.AllowGiftTag,
            result.SortOrder,
            result.CreatedAt,
            result.UnpublishedAt,
            result.Images,
            result.Version
        );
    }
}