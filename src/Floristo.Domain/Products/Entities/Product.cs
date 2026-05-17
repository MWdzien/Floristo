using Floristo.Domain.Categories.ValueObjects;
using Floristo.Domain.Products.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Products.Entities;

public class Product : AggregateRoot<ProductId>
{
    private ProductName _name;
    private ProductSlug _slug;
    private ProductDescription? _description;
    private ProductPrice _price;
    private CategoryId _categoryId;
    private ProductDimensions? _productDimensions;
    private ProductSeoTitle? _seoTitle;
    private ProductSeoDescription? _seoDescription;
    private bool _allowEngraving;
    private bool _allowGiftTag;
    private bool _isFeatured;
    private ProductSortOrder _sortOrder;
    private ProductCreatedAt _createdAt;
    private ProductDeactivatedAt? _deactivatedAt;
}