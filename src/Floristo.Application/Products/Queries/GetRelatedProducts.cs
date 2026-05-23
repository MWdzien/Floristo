using Floristo.Application.Products.DTOs;
using Floristo.SharedAbstractions.Queries;

namespace Floristo.Application.Products.Queries;

public record GetRelatedProducts(Guid ProductId, int Limit = 4) : IQuery<IReadOnlyList<StorefrontProductDto>>;