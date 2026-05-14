using Floristo.Domain.Categories.ValueObjects;
using Floristo.SharedAbstractions.Domain;

namespace Floristo.Domain.Categories.Events;

public record CategoryCreatedEvent(CategoryId Id, CategoryName Name, CategoryDescription? Description,
    CategorySlug Slug, CategorySortOrder SortOrder, CategoryImageUrl? ImageUrl, CategorySeoTitle? SeoTitle,
    CategorySeoDescription? SeoDescription, CategoryCreatedAt CreatedAt) : IDomainEvent;