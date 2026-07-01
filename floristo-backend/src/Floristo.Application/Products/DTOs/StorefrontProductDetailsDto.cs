namespace Floristo.Application.Products.DTOs;

public record StorefrontProductDetailsDto(
    Guid Id,
    string Name,
    string Slug,
    string? Description,
    decimal Price,
    string CategorySlug,
    string CategoryName,
    ProductDimensionsDto? Dimensions,
    string? SeoTitle,
    string? SeoDescription,
    bool AllowEngraving,
    bool AllowGiftTag,
    IReadOnlyList<ProductImageDto> Images
    );