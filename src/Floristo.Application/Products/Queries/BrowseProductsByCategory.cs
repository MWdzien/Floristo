using Floristo.Application.Products.Consts;
using Floristo.Application.Products.DTOs;
using Floristo.SharedAbstractions.Paging;
using Floristo.SharedAbstractions.Queries;

namespace Floristo.Application.Products.Queries;

public record BrowseProductsByCategory(string CategorySlug, ProductSortOption Sort = ProductSortOption.SortOrder) 
    : PagedQuery, IQuery<PagedResult<StorefrontProductDto>>;