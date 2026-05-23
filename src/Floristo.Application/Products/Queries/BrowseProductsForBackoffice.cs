using Floristo.Application.Products.Consts;
using Floristo.Application.Products.DTOs;
using Floristo.SharedAbstractions.Paging;
using Floristo.SharedAbstractions.Queries;

namespace Floristo.Application.Products.Queries;

public record BrowseProductsForBackoffice(string? SearchPhrase, Guid? CategoryId, bool? IsPublished, 
    ProductSortOption Sort = ProductSortOption.NewestFirst) : PagedQuery, IQuery<PagedResult<BackofficeProductDto>>;