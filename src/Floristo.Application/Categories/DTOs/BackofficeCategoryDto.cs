namespace Floristo.Application.Categories.DTOs;

public record BackofficeCategoryDto(
    Guid Id,
    string Name,
    string ImageUrl,
    bool IsPublished,
    int SortOrder
    );