namespace Floristo.Application.Products.DTOs;

public record BackofficeProductDto(
    Guid Id,
    string Name,
    string Slug,
    decimal Price,
    string CategoryName,
    bool IsPublished,
    string MainImageUrl
    );