using Floristo.Domain.Categories.ValueObjects;
using Floristo.Domain.Products.Events;
using Floristo.Domain.Products.Exceptions;
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

    private Product()
    {
    }

    public Product(
        ProductId id,
        ProductName name, 
        ProductSlug slug, 
        ProductPrice price, 
        CategoryId categoryId, 
        bool allowEngraving, 
        bool allowGiftTag, 
        bool isFeatured, 
        ProductSortOrder sortOrder
        )
    {
        SetState(id, name, slug, price, categoryId, allowEngraving, allowGiftTag, isFeatured, sortOrder);
        
        _createdAt = DateTime.UtcNow;
        _deactivatedAt = DateTime.UtcNow;
        
        AddEvent(new ProductCreatedEvent(id, name, slug, price, categoryId, allowEngraving, allowGiftTag, isFeatured, sortOrder, _createdAt, _deactivatedAt));
    }

    public Product(
        ProductId id,
        ProductName name, 
        ProductSlug slug, 
        ProductDescription? description, 
        ProductPrice price, 
        CategoryId categoryId, 
        ProductDimensions? productDimensions, 
        ProductSeoTitle? seoTitle, 
        ProductSeoDescription? seoDescription, 
        bool allowEngraving, 
        bool allowGiftTag, 
        bool isFeatured, 
        ProductSortOrder sortOrder, 
        ProductCreatedAt createdAt, 
        ProductDeactivatedAt? deactivatedAt
        )
    {
        SetState(id, name, slug, price, categoryId, allowEngraving, allowGiftTag, isFeatured, sortOrder);
        
        _description = description;
        _productDimensions = productDimensions;
        _seoTitle = seoTitle;
        _seoDescription = seoDescription;
        
        _createdAt = createdAt;
        _deactivatedAt = deactivatedAt;
    }

    private void SetState(ProductId id, ProductName name, ProductSlug slug, ProductPrice price, CategoryId categoryId, 
        bool allowEngraving, bool allowGiftTag, bool isFeatured, ProductSortOrder sortOrder)
    {
        Id = id;
        _name = name;
        _slug = slug;
        _price = price;
        _categoryId = categoryId;
        _allowEngraving = allowEngraving;
        _allowGiftTag = allowGiftTag;
        _isFeatured = isFeatured;
        _sortOrder = sortOrder;
    }

    public void ChangeName(ProductName name)
    {
        _name = name;
        AddEvent(new ProductNameChangedEvent(Id, name));
    }

    public void ChangeSlug(ProductSlug slug)
    {
        if (_slug == slug)
        {
            throw new ProductSlugAlreadyInUseException();
        }
        
        _slug = slug;
        AddEvent(new ProductSlugChangedEvent(Id, slug));
    }

    public void ChangeDescription(ProductDescription description)
    {
        _description = description;
        AddEvent(new ProductDescriptionChangedEvent(Id, description));
    }

    public void DeleteDescription()
    {
        _description = null;
        AddEvent(new ProductDescriptionChangedEvent(Id, null));
    }

    public void ChangePrice(ProductPrice price)
    {
        _price = price;
        AddEvent(new ProductPriceChangedEvent(Id, price));
    }
    
    public void ChangeCategory(CategoryId categoryId)
    {
        _categoryId = categoryId;
        AddEvent(new ProductCategoryChangedEvent(Id, categoryId));
    }

    public void ChangeDimensions(ProductDimensions dimensions)
    {
        _productDimensions = dimensions;
        AddEvent(new ProductDimensionsChangedEvent(Id, dimensions));
    }

    public void DeleteDimensions()
    {
        _productDimensions = null;
        AddEvent(new ProductDimensionsChangedEvent(Id, null));
    }

    public void ChangeSeoTitle(ProductSeoTitle seoTitle)
    {
        _seoTitle = seoTitle;
        AddEvent(new ProductSeoTitleChangedEvent(Id, seoTitle));
    }

    public void DeleteSeoTitle()
    {
        _seoTitle = null;
        AddEvent(new ProductSeoTitleChangedEvent(Id, null));
    }

    public void ChangeSeoDescription(ProductSeoDescription seoDescription)
    {
        _seoDescription = seoDescription;
        AddEvent(new ProductSeoDescriptionChangedEvent(Id, seoDescription));
    }

    public void DeleteSeoDescription()
    {
        _seoDescription = null;
        AddEvent(new ProductSeoDescriptionChangedEvent(Id, null));
    }

    public void AllowEngraving()
    {
        _allowEngraving = true;
        AddEvent(new ProductEngravingAllowedEvent(Id));
    }

    public void DisallowEngraving()
    {
        _allowEngraving = false;
        AddEvent(new ProductEngravingDisallowedEvent(Id));
    }

    public void AllowGiftTag()
    {
        _allowGiftTag = true;
        AddEvent(new ProductGiftTagAllowedEvent(Id));
    }

    public void DisallowGiftTag()
    {
        _allowGiftTag = false;
        AddEvent(new ProductGiftTagDisallowedEvent(Id));
    }

    public void AddToFeatured()
    {
        _isFeatured = true;
        AddEvent(new ProductAddedToFeaturedEvent(Id));
    }
    
    public void RemoveFromFeatured()
    {
        _isFeatured = false;
        AddEvent(new ProductRemovedFromFeaturedEvent(Id));
    }

    public void ChangeSortOrder(ProductSortOrder sortOrder)
    {
        _sortOrder = sortOrder;
        AddEvent(new ProductSortOrderChangedEvent(Id, sortOrder));
    }

    public void Deactivate()
    {
        if (_deactivatedAt is not null)
        {
            throw new ProductAlreadyDeactivatedException();
        }
        
        var deactivatedAt = DateTime.UtcNow;
        _deactivatedAt = deactivatedAt;
        AddEvent(new ProductDeactivatedEvent(Id, deactivatedAt));
    }

    public void Activate()
    {
        if (_deactivatedAt == null)
        {
            throw new ProductAlreadyActiveException();
        }

        _deactivatedAt = null;
        AddEvent(new ProductActivatedEvent(Id));
    }
    
    public void Delete()
    {
        if (_deactivatedAt == null)
        {
            throw new CannotDeleteActiveProductException();
        }
        
        AddEvent(new ProductDeletedEvent(Id));
    }
}