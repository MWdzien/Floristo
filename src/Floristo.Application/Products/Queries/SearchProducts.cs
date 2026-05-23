using Floristo.Application.Products.Consts;
using Floristo.Application.Products.DTOs;
using Floristo.SharedAbstractions.Paging;
using Floristo.SharedAbstractions.Queries;

namespace Floristo.Application.Products.Queries;

public record SearchProducts(string Query, ProductSortOption Sort = ProductSortOption.NewestFirst)
    : PagedQuery, IQuery<PagedResult<StorefrontProductDto>>;