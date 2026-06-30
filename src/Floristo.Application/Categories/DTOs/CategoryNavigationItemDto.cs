namespace Floristo.Application.Categories.DTOs;

public record CategoryNavigationItemDto(string Name, string Slug, string? ImageUrl, IReadOnlyList<CategoryNavigationItemDto> Children);