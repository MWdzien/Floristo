namespace Floristo.Application.Products.DTOs;

public record BackofficeProductDetailsDto
(
    Guid Id,
    string Name,
    string Slug,
    string? Description,
    decimal Price,
    Guid CategoryId,
    string CategoryName,
    ProductDimensionsDto? Dimensions,
    string? SeoTitle,
    string? SeoDescription,
    bool AllowEngraving,
    bool AllowGiftTag,
    int SortOrder,
    DateTime CreatedAt,
    DateTime? PublishedAt,
    IReadOnlyList<ProductImageDto> Images,
    int Version
);