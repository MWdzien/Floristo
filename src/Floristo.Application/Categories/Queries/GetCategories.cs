using Floristo.Application.Categories.DTOs;
using Floristo.SharedAbstractions.Queries;

namespace Floristo.Application.Categories.Queries;

public record GetCategories(string? SearchPhrase, bool? IsActive) : IQuery<IEnumerable<CategoryDto>>;