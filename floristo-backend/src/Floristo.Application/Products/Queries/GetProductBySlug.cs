using Floristo.Application.Products.DTOs;
using Floristo.SharedAbstractions.Queries;

namespace Floristo.Application.Products.Queries;

public record GetProductBySlug(string Slug) : IQuery<StorefrontProductDetailsDto>;