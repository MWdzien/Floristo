using Floristo.Application.Products.Consts;
using Floristo.Application.Products.DTOs;
using Floristo.Infrastructure.EF.Products.Models;

namespace Floristo.Infrastructure.EF.Products.Queries;

internal static class Extensions
{
    public static IQueryable<ProductReadModel> ApplySort(this IQueryable<ProductReadModel> query,
        ProductSortOption sort)
        => sort switch
        {
            ProductSortOption.SortOrder => query.OrderBy(p => p.SortOrder).ThenBy(p => p.Id),
            ProductSortOption.NewestFirst => query.OrderByDescending(p => p.CreatedAt).ThenBy(p => p.Id),
            ProductSortOption.OldestFirst => query.OrderBy(p => p.CreatedAt).ThenBy(p => p.Id),
            ProductSortOption.PriceAsc => query.OrderBy(p => p.Price).ThenBy(p => p.Id),
            ProductSortOption.PriceDesc => query.OrderByDescending(p => p.Price).ThenBy(p => p.Id),
            ProductSortOption.NameAsc => query.OrderBy(p => p.Name).ThenBy(p => p.Id),
            _ => throw new ArgumentOutOfRangeException(nameof(sort))
        };
    
    internal static StorefrontProductDto AsDto(this ProductReadModel product)                                                                                                                                             
        => new(product.Name, product.Slug, product.Price, 
            product.Images.OrderBy(i => i.SortOrder).Select(i => i.Url).First());
}