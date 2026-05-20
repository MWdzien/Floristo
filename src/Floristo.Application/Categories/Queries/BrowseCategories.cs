using Floristo.Application.Categories.DTOs;
using Floristo.SharedAbstractions.Queries;

namespace Floristo.Application.Categories.Queries;

public record BrowseCategories() : IQuery<IEnumerable<StorefrontCategoryDto>>;