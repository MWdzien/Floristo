using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Products.Commands;

public record UpdateProductDetails(
    Guid Id,
    string Name,
    string Slug,
    string? Description, 
    decimal? Height, decimal? Width, decimal? Depth, decimal? Diameter, decimal? Length,
    string? SeoTitle,
    string? SeoDescription
    ) : ICommand;