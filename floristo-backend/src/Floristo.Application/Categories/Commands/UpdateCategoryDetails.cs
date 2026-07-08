using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands;

public record UpdateCategoryDetails(
    Guid Id,
    string Name,
    string Slug,
    string? ShortDescription,
    string? Description,
    string? ImageUrl,
    string? SeoTitle,
    string? SeoDescription
    ) : ICommand;
