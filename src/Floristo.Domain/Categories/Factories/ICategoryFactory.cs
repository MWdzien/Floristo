using Floristo.Domain.Categories.Entities;
using Floristo.Domain.Categories.ValueObjects;

namespace Floristo.Domain.Categories.Factories;

public interface ICategoryFactory
{
    Category Create(CategoryId id, CategoryName name, CategoryDescription? description, CategorySlug slug, 
        CategorySortOrder sortOrder, CategoryImageUrl? imageUrl, CategorySeoTitle? seoTitle, 
        CategorySeoDescription? seoDescription);
    Category Restore(CategoryId id, CategoryName name, CategoryDescription? description, CategorySlug slug, 
        CategorySortOrder sortOrder, CategoryImageUrl? imageUrl, CategorySeoTitle? seoTitle, CategorySeoDescription? 
            seoDescription, CategoryCreatedAt createdAt, CategoryDeactivatedAt? deactivatedAt);
}