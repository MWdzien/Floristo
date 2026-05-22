using Floristo.Domain.Categories.ValueObjects;
using Floristo.Domain.Products.Events;
using Floristo.Domain.Products.Exceptions;
using Floristo.Domain.Products.Exceptions.StateChanged;
using Floristo.Domain.Products.Exceptions.Validation.Image;
using Floristo.Domain.Products.ValueObjects;
using Floristo.Domain.Products.ValueObjects.ProductImage;
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
    private ProductSortOrder _sortOrder;
    private ProductCreatedAt _createdAt;
    private ProductUnpublishedAt? _unpublishedAt;

    private readonly List<ProductImage> _images = new();
    public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly();

    public const int MaxImages = 10;

    private Product()
    {
    }

    internal Product(
        ProductId id,
        ProductName name, 
        ProductSlug slug, 
        ProductPrice price, 
        CategoryId categoryId,
        ProductSortOrder sortOrder
        )
    {
        SetState(id, name, slug, price, categoryId, sortOrder);

        _allowEngraving = false;
        _allowGiftTag = false;
        _createdAt = DateTime.UtcNow;
        _unpublishedAt = DateTime.UtcNow;
        
        AddEvent(new ProductCreatedEvent(id, name, slug, price, categoryId, _allowEngraving, _allowGiftTag, sortOrder, _createdAt, _unpublishedAt));
    }

    internal Product(
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
        ProductSortOrder sortOrder, 
        ProductCreatedAt createdAt, 
        ProductUnpublishedAt? unpublishedAt,
        IEnumerable<ProductImage> images
        )
    {
        SetState(id, name, slug, price, categoryId, sortOrder);
        
        _description = description;
        _productDimensions = productDimensions;
        _seoTitle = seoTitle;
        _seoDescription = seoDescription;
        _allowEngraving = allowEngraving;
        _allowGiftTag = allowGiftTag;
        
        _createdAt = createdAt;
        _unpublishedAt = unpublishedAt;

        _images = images.ToList();
    }

    private void SetState(ProductId id, ProductName name, ProductSlug slug, ProductPrice price, 
        CategoryId categoryId, ProductSortOrder sortOrder)
    {
        Id = id;
        _name = name;
        _slug = slug;
        _price = price;
        _categoryId = categoryId;
        _sortOrder = sortOrder;
    }

    public void ChangeName(ProductName name)
    {
        if (_name == name)
        {
            throw new ProductNameNotChangedException();
        }
        
        _name = name;
        AddEvent(new ProductNameChangedEvent(Id, name));
    }

    public void ChangeSlug(ProductSlug slug)
    {
        if (_slug == slug)
        {
            throw new ProductSlugNotChangedException();
        }
        
        _slug = slug;
        AddEvent(new ProductSlugChangedEvent(Id, slug));
    }

    public void ChangeDescription(ProductDescription description)
    {
        if (_description == description)
        {
            throw new ProductDescriptionNotChangedException();
        }
        
        _description = description;
        AddEvent(new ProductDescriptionChangedEvent(Id, description));
    }

    public void DeleteDescription()
    {
        if (_description is null)
        {
            throw new ProductDescriptionNotChangedException();
        }
        
        _description = null;
        AddEvent(new ProductDescriptionChangedEvent(Id, null));
    }

    public void ChangePrice(ProductPrice price)
    {
        if (_price == price)
        {
            throw new ProductPriceNotChangedException();
        }
        
        _price = price;
        AddEvent(new ProductPriceChangedEvent(Id, price));
    }
    
    public void ChangeCategory(CategoryId categoryId)
    {
        if (_categoryId == categoryId)
        {
            throw new ProductCategoryNotChangedException();
        }
        
        _categoryId = categoryId;
        AddEvent(new ProductCategoryChangedEvent(Id, categoryId));
    }

    public void ChangeDimensions(ProductDimensions dimensions)
    {
        if (_productDimensions == dimensions)
        {
            throw new ProductDimensionsNotChangedException();
        }
        
        _productDimensions = dimensions;
        AddEvent(new ProductDimensionsChangedEvent(Id, dimensions));
    }

    public void DeleteDimensions()
    {
        if (_productDimensions is null)
        {
            throw new ProductDimensionsNotChangedException();
        }
        
        _productDimensions = null;
        AddEvent(new ProductDimensionsChangedEvent(Id, null));
    }

    public void ChangeSeoTitle(ProductSeoTitle seoTitle)
    {
        if (_seoTitle == seoTitle)
        {
            throw new ProductSeoTitleNotChangedException();
        }
        
        _seoTitle = seoTitle;
        AddEvent(new ProductSeoTitleChangedEvent(Id, seoTitle));
    }

    public void DeleteSeoTitle()
    {
        if (_seoTitle is null)
        {
            throw new ProductSeoTitleNotChangedException();
        }
        
        _seoTitle = null;
        AddEvent(new ProductSeoTitleChangedEvent(Id, null));
    }

    public void ChangeSeoDescription(ProductSeoDescription seoDescription)
    {
        if (_seoDescription == seoDescription)
        {
            throw new ProductSeoDescriptionNotChangedException();
        }
        
        _seoDescription = seoDescription;
        AddEvent(new ProductSeoDescriptionChangedEvent(Id, seoDescription));
    }

    public void DeleteSeoDescription()
    {
        if (_seoDescription is null)
        {
            throw new ProductSeoDescriptionNotChangedException();
        }
        
        _seoDescription = null;
        AddEvent(new ProductSeoDescriptionChangedEvent(Id, null));
    }

    public void AllowEngraving()
    {
        if (_allowEngraving)
        {
            throw new ProductEngravingAlreadyAllowedException();
        }
        
        _allowEngraving = true;
        AddEvent(new ProductEngravingAllowedEvent(Id));
    }

    public void DisallowEngraving()
    {
        if (!_allowEngraving)
        {
            throw new ProductEngravingAlreadyDisallowedException();
        }
        
        _allowEngraving = false;
        AddEvent(new ProductEngravingDisallowedEvent(Id));
    }

    public void AllowGiftTag()
    {
        if (_allowGiftTag)
        {
            throw new ProductGiftTagAlreadyAllowedException();
        }
        
        _allowGiftTag = true;
        AddEvent(new ProductGiftTagAllowedEvent(Id));
    }

    public void DisallowGiftTag()
    {
        if (!_allowGiftTag)
        {
            throw new ProductGiftTagAlreadyDisallowedException();
        }
        
        _allowGiftTag = false;
        AddEvent(new ProductGiftTagDisallowedEvent(Id));
    }

    public void ChangeSortOrder(ProductSortOrder sortOrder)
    {
        if (_sortOrder == sortOrder)
        {
            throw new ProductSortOrderNotChangedException();
        }
        
        _sortOrder = sortOrder;
        AddEvent(new ProductSortOrderChangedEvent(Id, sortOrder));
    }

    public void Unpublish()
    {
        if (_unpublishedAt is not null)
        {
            throw new ProductAlreadyUnpublishedException();
        }
        
        var unpublishedAt = DateTime.UtcNow;
        _unpublishedAt = unpublishedAt;
        AddEvent(new ProductUnpublishedEvent(Id, unpublishedAt));
    }

    public void Publish()
    {
        if (_unpublishedAt == null)
        {
            throw new ProductAlreadyPublishedException();
        }

        if (_images.Count == 0)
            throw new CannotPublishProductWithoutImagesException();

        _unpublishedAt = null;
        AddEvent(new ProductPublishedEvent(Id));
    }

    public void AddImage(ProductImageId imageId, ProductImageUrl url, ProductImageSortOrder sortOrder)
    {
        if (_images.Count >= MaxImages)
            throw new ProductImageLimitExceededException();

        if (_images.Any(i => i.Id == imageId))
            throw new ProductImageAlreadyExistsException();

        if (_images.Any(i => i.SortOrder == sortOrder))
            throw new ProductImageSortOrderAlreadyTakenException();
        
        _images.Add(new ProductImage(imageId, url, sortOrder));
        AddEvent(new ProductImageAddedEvent(Id, imageId, url, sortOrder));
    }

    public void RemoveImage(ProductImageId imageId)
    {
        var image = _images.FirstOrDefault(i => i.Id == imageId) 
                    ?? throw new ProductImageNotFoundException();

        if (_unpublishedAt is null && _images.Count == 1)
            throw new CannotRemoveLastImageOfPublishedProductException();

        _images.Remove(image);
        AddEvent(new ProductImageRemovedEvent(Id, imageId));
    }

    public void ChangeImageSortOrder(ProductImageId imageId, ProductImageSortOrder sortOrder)
    {
        var image = _images.FirstOrDefault(i => i.Id == imageId) 
                    ?? throw new ProductImageNotFoundException();

        if (_images.Any(i => i.Id != imageId && i.SortOrder == sortOrder))
            throw new ProductImageSortOrderAlreadyTakenException();
        
        image.ChangeSortOrder(sortOrder);
        AddEvent(new ProductImageSortOrderChangedEvent(Id, imageId, sortOrder));
    }

    public void Delete()
    {
        if (_unpublishedAt == null)
        {
            throw new CannotDeletePublishedProductException();
        }
        
        AddEvent(new ProductDeletedEvent(Id));
    }
}