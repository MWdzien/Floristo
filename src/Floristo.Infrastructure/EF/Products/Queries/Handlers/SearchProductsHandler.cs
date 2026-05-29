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

internal sealed class SearchProductsHandler : IQueryHandler<SearchProducts, PagedResult<StorefrontProductDto>>
{
    private readonly DbSet<ProductReadModel> _products;
    private readonly DbSet<CategoryReadModel> _categories;

    public SearchProductsHandler(ReadDbContext dbContext)
    {
        _products = dbContext.Products;
        _categories = dbContext.Categories;
    }

    public async Task<PagedResult<StorefrontProductDto>> HandleAsync(SearchProducts query)
    {
        var phrase = query.SearchPhrase?.Trim();
        
        if (string.IsNullOrWhiteSpace(phrase))
            return PagedResult<StorefrontProductDto>.Empty(query);

        var matchingCategoryIds = _categories
            .Where(c => Microsoft.EntityFrameworkCore.EF.Functions.ILike(c.Name, $"%{phrase}%"))
            .Select(c => c.Id);

        var products = _products
            .AsNoTracking()
            .Where(p => p.PublishedAt != null
                        && (p.SearchVector.Matches(
                            Microsoft.EntityFrameworkCore.EF.Functions.PlainToTsQuery("simple", phrase))
                            || matchingCategoryIds.Contains(p.CategoryId)));

        var sorted = products.ApplySort(query.Sort);

        var result = sorted.AsDto();

        return await result.ToPagedResultAsync(query);
    }
}