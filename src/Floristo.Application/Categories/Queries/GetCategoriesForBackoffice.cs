using Floristo.Application.Categories.DTOs;
using Floristo.SharedAbstractions.Queries;

namespace Floristo.Application.Categories.Queries;

public record GetCategoriesForBackoffice(string? SearchPhrase, bool? IsPublished) : IQuery<IEnumerable<BackofficeCategoryDto>>;