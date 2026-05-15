using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands;

public record UpdateCategoryDetails(
    Guid Id,
    string? Description,
    string? ImageUrl,
    string? SeoTitle,
    string? SeoDescription) : ICommand;
