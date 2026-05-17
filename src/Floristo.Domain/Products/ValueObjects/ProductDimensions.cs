using Floristo.Domain.Products.Exceptions;

namespace Floristo.Domain.Products.ValueObjects;

public record ProductDimensions
{
    public decimal? Height { get; }
    public decimal? Width { get; }
    public decimal? Depth { get; }
    public decimal? Diameter { get; }
    public decimal? Length { get; }

    public ProductDimensions(decimal? height, decimal? width, decimal? depth, decimal? diameter, decimal? length)
    {
        if (height is null && width is null && depth is null && diameter is null && length is null)
        {
            throw new EmptyProductDimensionsException();
        }
        
        if (height is <= 0 or > 500)
            throw new InvalidProductDimensionException(nameof(height));
        if (width is <= 0 or > 500)
            throw new InvalidProductDimensionException(nameof(width));
        if (depth is <= 0 or > 500)
            throw new InvalidProductDimensionException(nameof(depth));
        if (diameter is <= 0 or > 500)
            throw new InvalidProductDimensionException(nameof(diameter));
        if (length is <= 0 or > 500)
            throw new InvalidProductDimensionException(nameof(length));
        
        Height = height;
        Width = width;
        Depth = depth;
        Diameter = diameter;
        Length = length;
    }
}