using Floristo.Application.Categories.DTOs;
using Floristo.Domain.Categories.Entities;
using Floristo.SharedAbstractions.Queries;

namespace Floristo.Application.Categories.Queries;

public record GetCategoryBySlug(string Slug) : IQuery<StorefrontCategoryDetailsDto?>;