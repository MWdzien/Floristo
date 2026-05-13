using Floristo.Domain.Categories.Events;
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

    internal Category(CategoryId id, CategoryName name, CategoryDescription? description, CategorySlug slug, 
        CategorySortOrder sortOrder, CategoryImageUrl? imageUrl, CategorySeoTitle? seoTitle, 
        CategorySeoDescription? seoDescription)
    {
        SetState(id, name, description, slug, sortOrder, imageUrl, seoTitle, seoDescription);
        _createdAt = DateTime.UtcNow;
        AddEvent(new CategoryCreatedEvent(id, name, description, slug, sortOrder, imageUrl, seoTitle, 
            seoDescription, _createdAt));
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
}