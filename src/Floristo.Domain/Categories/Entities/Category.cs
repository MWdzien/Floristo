using Floristo.Domain.Categories.Events;
using Floristo.Domain.Categories.Exceptions;
using Floristo.Domain.Categories.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Categories.Entities;

public class Category : AggregateRoot<CategoryId>
{
    private CategoryId? _parentId;
    
    private CategoryName _name;
    private CategoryDescription? _description;
    private CategorySlug _slug;
    private CategorySortOrder _sortOrder;
    private CategoryImageUrl? _imageUrl;
    private CategorySeoTitle?  _seoTitle;
    private CategorySeoDescription? _seoDescription;
    
    private CategoryCreatedAt _createdAt;
    private CategoryPublishedAt? _publishedAt;

    private Category()
    {
    }

    internal Category(CategoryId id, CategoryId? parentId, CategoryName name, CategorySlug slug, CategorySortOrder sortOrder)
    {
        Id = id;
        _parentId = parentId;
        _name = name;
        _slug = slug;
        _sortOrder = sortOrder;
        _createdAt = DateTime.UtcNow;
        AddEvent(new CategoryCreatedEvent(id, parentId, name, slug, sortOrder, _createdAt));
    }
    
    internal Category(CategoryId id, CategoryId? parentId, CategoryName name, CategoryDescription? description, CategorySlug slug, 
        CategorySortOrder sortOrder, CategoryImageUrl? imageUrl, CategorySeoTitle? seoTitle, CategorySeoDescription? 
            seoDescription, CategoryCreatedAt createdAt, CategoryPublishedAt? publishedAt)
    {
        SetState(id, parentId, name, description, slug, sortOrder, imageUrl, seoTitle, seoDescription);
        _createdAt = createdAt;
        _publishedAt = publishedAt;
    }

    private void SetState(CategoryId id, CategoryId? parentId, CategoryName name, CategoryDescription? description, CategorySlug slug,
        CategorySortOrder sortOrder, CategoryImageUrl? imageUrl, CategorySeoTitle? seoTitle,
        CategorySeoDescription? seoDescription)
    {
        Id = id;
        _parentId = parentId;
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
        if (_publishedAt == null)
        {
            throw new CategoryAlreadyUnpublishedException();
        }
        
        _publishedAt = null;
        AddEvent(new CategoryUnpublishedEvent(Id));
    }

    public void Publish()
    {
        if (_publishedAt != null)
        {
            throw new CategoryAlreadyPublishedException();
        }

        var publishedAt = DateTime.UtcNow;
        _publishedAt = publishedAt;
        AddEvent(new CategoryPublishedEvent(Id, publishedAt));
    }

    public void Delete()
    {
        if (_publishedAt != null)
        {
            throw new CannotDeletePublishedCategoryException();
        }
        
        AddEvent(new CategoryDeletedEvent(Id));
    }
}