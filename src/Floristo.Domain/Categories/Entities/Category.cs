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
}