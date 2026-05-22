using Floristo.Domain.Products.Exceptions.ProductImage;
using Floristo.Domain.Products.ValueObjects.ProductImage;

namespace Floristo.Domain.Products.Entities;

public class ProductImage
{
    public ProductImageId Id { get; private set; }
    private ProductImageUrl _url;
    private ProductImageSortOrder _sortOrder;

    internal ProductImageSortOrder SortOrder => _sortOrder;
    
    private ProductImage()
    {
    }

    internal ProductImage(ProductImageId id, ProductImageUrl url, ProductImageSortOrder sortOrder)
    {
        Id = id;
        _url = url;
        _sortOrder = sortOrder;
    }

    internal void ChangeSortOrder(ProductImageSortOrder sortOrder)
    {
        if (_sortOrder == sortOrder)
        {
            throw new ProductImageSortOrderNotChangedException();
        }

        _sortOrder = sortOrder;
    }
}