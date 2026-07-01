namespace Floristo.Application.Products.DTOs;

public record StorefrontProductDto(
    string Name,
    string Slug,
    decimal Price,
    string MainImageUrl
    );