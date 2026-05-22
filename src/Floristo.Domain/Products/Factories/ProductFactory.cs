using Floristo.Domain.Categories.ValueObjects;
using Floristo.Domain.Products.Entities;
using Floristo.Domain.Products.ValueObjects;

namespace Floristo.Domain.Products.Factories;

public sealed class ProductFactory : IProductFactory
{
    public Product Create(ProductId id, ProductName name, ProductSlug slug, ProductPrice price, CategoryId categoryId,
        ProductSortOrder sortOrder)
        => new(id, name, slug, price, categoryId, sortOrder);

    public Product Restore(ProductId id, ProductName name, ProductSlug slug, ProductDescription? description,
        ProductPrice price,
        CategoryId categoryId, ProductDimensions? productDimensions, ProductSeoTitle? seoTitle,
        ProductSeoDescription? seoDescription, bool allowEngraving, bool allowGiftTag, bool isFeatured,
        ProductSortOrder sortOrder, ProductCreatedAt createdAt, ProductUnpublishedAt? unpublishedAt)
        => new(id, name, slug, description, price, categoryId, productDimensions, seoTitle, seoDescription,
            allowEngraving, allowGiftTag, isFeatured, sortOrder, createdAt, unpublishedAt);
}