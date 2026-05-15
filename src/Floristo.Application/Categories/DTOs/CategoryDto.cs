namespace Floristo.Application.Categories.DTOs;

public record CategoryDto(Guid Id, string Name, string? Description, string Slug, int SortOrder, 
    string? ImageUrl, string? SeoTitle, string? SeoDescription, DateTime CreatedAt, DateTime? DeactivatedAt);