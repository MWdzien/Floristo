namespace Floristo.Application.Categories.DTOs;

public record BackofficeCategoryDetailsDto
(
    Guid Id,
    string Name,
    string? ShortDescription,
    string? Description,
    string Slug,
    int SortOrder,
    string? ImageUrl,
    string? SeoTitle,
    string? SeoDescription,
    DateTime CreatedAt,
    DateTime? PublishedAt
);