using Floristo.Domain.Categories.Events;
using Floristo.Domain.Categories.Exceptions;
using Floristo.Domain.Categories.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Categories.Entities;

public class Category : AggregateRoot<CategoryId>
{
    private CategoryName _name;
    private CategoryDescription? _description;
    private CategorySlug _slug;
    private CategorySortOrder _sortOrder;
    private CategoryImageUrl? _imageUrl;
    private CategorySeoTitle?  _seoTitle;
    private CategorySeoDescription? _seoDescription;
    
    private CategoryCreatedAt _createdAt;
    private CategoryUnpublishedAt? _unpublishedAt;

    private Category()
    {
    }

    internal Category(CategoryId id, CategoryName name, CategorySlug slug, CategorySortOrder sortOrder)
    {
        Id = id;
        _name = name;
        _slug = slug;
        _sortOrder = sortOrder;
        _createdAt = DateTime.UtcNow;
        _unpublishedAt = DateTime.UtcNow;
        AddEvent(new CategoryCreatedEvent(id, name, slug, sortOrder, _createdAt, _unpublishedAt));
    }
    
    internal Category(CategoryId id, CategoryName name, CategoryDescription? description, CategorySlug slug, 
        CategorySortOrder sortOrder, CategoryImageUrl? imageUrl, CategorySeoTitle? seoTitle, CategorySeoDescription? 
            seoDescription, CategoryCreatedAt createdAt, CategoryUnpublishedAt? unpublishedAt)
    {
        SetState(id, name, description, slug, sortOrder, imageUrl, seoTitle, seoDescription);
        _createdAt = createdAt;
        _unpublishedAt = unpublishedAt;
    }

    private void SetState(CategoryId id, CategoryName name, CategoryDescription? description, CategorySlug slug,
        CategorySortOrder sortOrder, CategoryImageUrl? imageUrl, CategorySeoTitle? seoTitle,
        CategorySeoDescription? seoDescription)
    {
        Id = id;
        _name = name;
        _description = description;
        _slug = slug;
        _sortOrder = sortOrder;
        _imageUrl = imageUrl;
        _seoTitle = seoTitle;
        _seoDescription = seoDescription;
    }

    public void ChangeName(CategoryName name)
    {
        if (_name == name) return;
        
        _name = name;
        AddEvent(new CategoryNameChangedEvent(Id, name));
    }

    public void ChangeDescription(CategoryDescription description)
    {
        if (_description == description) return;
        
        _description = description;
        AddEvent(new CategoryDescriptionChangedEvent(Id, description));
    }

    public void DeleteDescription()
    {
        if (_description is null) return;
        
        _description = null;
        AddEvent(new CategoryDescriptionChangedEvent(Id, _description));
    }
    
    public void ChangeSlug(CategorySlug slug)
    {
        if (_slug == slug) return;
        
        _slug = slug;
        AddEvent(new CategorySlugChangedEvent(Id, slug));
    }
    
    public void ChangeSortOrder(CategorySortOrder sortOrder)
    {
        if (_sortOrder == sortOrder) return;
        
        _sortOrder = sortOrder;
        AddEvent(new CategorySortOrderChangedEvent(Id, sortOrder));
    }
    
    public void ChangeImageUrl(CategoryImageUrl imageUrl)
    {
        if (_imageUrl == imageUrl) return;
        
        _imageUrl = imageUrl;
        AddEvent(new CategoryImageUrlChangedEvent(Id, imageUrl));
    }

    public void DeleteImageUrl()
    {
        if (_imageUrl is null) return;
        
        _imageUrl = null;
        AddEvent(new CategoryImageUrlChangedEvent(Id, _imageUrl));
    }
    
    public void ChangeSeoTitle(CategorySeoTitle seoTitle)
    {
        if (_seoTitle == seoTitle) return;
        
        _seoTitle = seoTitle;
        AddEvent(new CategorySeoTitleChangedEvent(Id, seoTitle));
    }

    public void DeleteSeoTitle()
    {
        if (_seoTitle is null) return;
        
        _seoTitle = null;
        AddEvent(new CategorySeoTitleChangedEvent(Id, _seoTitle));
    }
    
    public void ChangeSeoDescription(CategorySeoDescription seoDescription)
    {
        if (_seoDescription == seoDescription) return;
        
        _seoDescription = seoDescription;
        AddEvent(new CategorySeoDescriptionChangedEvent(Id, seoDescription));
    }

    public void DeleteSeoDescription()
    {
        if (_seoDescription is null) return;
        
        _seoDescription = null;
        AddEvent(new CategorySeoDescriptionChangedEvent(Id, _seoDescription));
    }
    
    public void Unpublish()
    {
        if (_unpublishedAt != null)
        {
            throw new CategoryAlreadyUnpublishedException();
        }
        
        var unpublishedAt = DateTime.UtcNow;
        _unpublishedAt = unpublishedAt;
        AddEvent(new CategoryUnpublishedEvent(Id, unpublishedAt));
    }

    public void Publish()
    {
        if (_unpublishedAt == null)
        {
            throw new CategoryAlreadyPublishedException();
        }

        _unpublishedAt = null;
        AddEvent(new CategoryPublishedEvent(Id));
    }

    public void Delete()
    {
        if (_unpublishedAt == null)
        {
            throw new CannotDeletePublishedCategoryException();
        }
        
        AddEvent(new CategoryDeletedEvent(Id));
    }
}