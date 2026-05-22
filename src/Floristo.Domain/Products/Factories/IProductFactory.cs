using Floristo.Domain.Categories.ValueObjects;
using Floristo.Domain.Products.Entities;
using Floristo.Domain.Products.ValueObjects;

namespace Floristo.Domain.Products.Factories;

public interface IProductFactory
{
    Product Create(ProductId id, ProductName name, ProductSlug slug, ProductPrice price, CategoryId categoryId, 
        ProductSortOrder sortOrder);

    Product Restore(ProductId id, ProductName name, ProductSlug slug, ProductDescription? description,
        ProductPrice price, CategoryId categoryId, ProductDimensions? productDimensions, ProductSeoTitle? seoTitle,
        ProductSeoDescription? seoDescription, bool allowEngraving, bool allowGiftTag, bool isFeatured,
        ProductSortOrder sortOrder, ProductCreatedAt createdAt, ProductUnpublishedAt? unpublishedAt);
}