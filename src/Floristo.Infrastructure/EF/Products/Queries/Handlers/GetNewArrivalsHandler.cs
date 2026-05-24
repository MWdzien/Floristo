using Floristo.Application.Products.DTOs;
using Floristo.Application.Products.Queries;
using Floristo.Infrastructure.EF.Contexts;
using Floristo.Infrastructure.EF.Products.Models;
using Floristo.SharedAbstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Infrastructure.EF.Products.Queries.Handlers;

internal sealed class GetNewArrivalsHandler : IQueryHandler<GetNewArrivals, IReadOnlyList<StorefrontProductDto>>
{
    private readonly DbSet<ProductReadModel> _products;

    public GetNewArrivalsHandler(ReadDbContext dbContext)
    {
        _products = dbContext.Products;
    }

    public async Task<IReadOnlyList<StorefrontProductDto>> HandleAsync(GetNewArrivals query)
        => await _products
            .AsNoTracking()
            .Where(p => p.UnpublishedAt == null)
            .OrderByDescending(p => p.CreatedAt)
            .ThenBy(p => p.Id)
            .Take(query.Limit)
            .Select(p => new StorefrontProductDto(
                p.Name,
                p.Slug,
                p.Price,
                p.Images
                    .OrderBy(i => i.SortOrder)
                    .Select(i => i.Url)
                    .First()))
            .ToListAsync();
}