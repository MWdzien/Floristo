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
    private CategoryDeactivatedAt? _deactivatedAt;

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
        _deactivatedAt = DateTime.UtcNow;
        AddEvent(new CategoryCreatedEvent(id, name, slug, sortOrder, _createdAt, _deactivatedAt));
    }
    
    internal Category(CategoryId id, CategoryName name, CategoryDescription? description, CategorySlug slug, 
        CategorySortOrder sortOrder, CategoryImageUrl? imageUrl, CategorySeoTitle? seoTitle, CategorySeoDescription? 
            seoDescription, CategoryCreatedAt createdAt, CategoryDeactivatedAt? deactivatedAt)
    {
        SetState(id, name, description, slug, sortOrder, imageUrl, seoTitle, seoDescription);
        _createdAt = createdAt;
        _deactivatedAt = deactivatedAt;
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
        _name = name;
        AddEvent(new CategoryNameChangedEvent(Id, name));
    }

    public void ChangeDescription(CategoryDescription? description)
    {
        _description = description;
        AddEvent(new CategoryDescriptionChangedEvent(Id, description));
    }
    
    public void ChangeSlug(CategorySlug slug)
    {
        _slug = slug;
        AddEvent(new CategorySlugChangedEvent(Id, slug));
    }
    
    public void ChangeSortOrder(CategorySortOrder sortOrder)
    {
        _sortOrder = sortOrder;
        AddEvent(new CategorySortOrderChangedEvent(Id, sortOrder));
    }
    
    public void ChangeImageUrl(CategoryImageUrl? imageUrl)
    {
        _imageUrl = imageUrl;
        AddEvent(new CategoryImageUrlChangedEvent(Id, imageUrl));
    }
    
    public void ChangeSeoTitle(CategorySeoTitle? seoTitle)
    {
        _seoTitle = seoTitle;
        AddEvent(new CategorySeoTitleChangedEvent(Id, seoTitle));
    }
    
    public void ChangeSeoDescription(CategorySeoDescription? seoDescription)
    {
        _seoDescription = seoDescription;
        AddEvent(new CategorySeoDescriptionChangedEvent(Id, seoDescription));
    }
    
    public void Deactivate()
    {
        if (_deactivatedAt != null)
        {
            throw new CategoryAlreadyDeactivatedException();
        }
        
        var deactivatedAt = DateTime.UtcNow;
        _deactivatedAt = deactivatedAt;
        AddEvent(new CategoryDeactivatedEvent(Id, deactivatedAt));
    }

    public void Activate()
    {
        if (_deactivatedAt == null)
        {
            throw new CategoryAlreadyActiveException();
        }

        _deactivatedAt = null;
        AddEvent(new CategoryActivatedEvent(Id));
    }
}