using Floristo.Domain.Categories.Entities;
using Floristo.Domain.Categories.ValueObjects;

namespace Floristo.Domain.Categories.Factories;

public sealed class CategoryFactory : ICategoryFactory
{
    public Category Create(CategoryId id, CategoryId? parentId, CategoryName name, CategorySlug slug,
        CategorySortOrder sortOrder)
            => new(id, parentId, name, slug, sortOrder);

    public Category Restore(CategoryId id, CategoryId? parentId, CategoryName name, CategoryShortDescription? shortDescription, 
        CategoryDescription? description, CategorySlug slug, CategorySortOrder sortOrder, CategoryImageUrl? imageUrl, 
        CategorySeoTitle? seoTitle, CategorySeoDescription? seoDescription, CategoryCreatedAt createdAt, 
        CategoryPublishedAt? publishedAt)
            => new(id, parentId, name, shortDescription, description, slug, sortOrder, imageUrl, seoTitle, 
                seoDescription, createdAt, publishedAt);
}