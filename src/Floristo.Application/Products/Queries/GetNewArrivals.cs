using Floristo.Application.Products.DTOs;
using Floristo.SharedAbstractions.Queries;

namespace Floristo.Application.Products.Queries;

public record GetNewArrivals(int Limit = 8) : IQuery<IReadOnlyList<StorefrontProductDto>>;