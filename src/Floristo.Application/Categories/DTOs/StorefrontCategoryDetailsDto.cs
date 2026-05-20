namespace Floristo.Application.Categories.DTOs;

public record StorefrontCategoryDetailsDto(
    string Name,
    string Slug,
    string? Description,
    string? ImageUrl,
    string? SeoTitle,
    string? SeoDescription
    );