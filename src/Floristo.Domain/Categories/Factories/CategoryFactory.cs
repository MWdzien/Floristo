using Floristo.Domain.Categories.Entities;
using Floristo.Domain.Categories.ValueObjects;

namespace Floristo.Domain.Categories.Factories;

public sealed class CategoryFactory : ICategoryFactory
{
    public Category Create(CategoryId id, CategoryName name, CategorySlug slug,
        CategorySortOrder sortOrder)
            => new(id, name, slug, sortOrder);

    public Category Restore(CategoryId id, CategoryName name, CategoryDescription? description, CategorySlug slug,
        CategorySortOrder sortOrder, CategoryImageUrl? imageUrl, CategorySeoTitle? seoTitle,
        CategorySeoDescription? seoDescription, CategoryCreatedAt createdAt, CategoryPublishedAt? publishedAt)
            => new(id, name, description, slug, sortOrder, imageUrl, seoTitle, seoDescription, createdAt, publishedAt);
}